using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.Collections;

namespace OSX.WmiLib
{
    [DebuggerDisplay("{ID}")]
    internal abstract class WmiObject
    {
        protected ManagementObject m_wmiObject;
        protected WmiContext m_Context;
        protected Dictionary<Type, IEnumerable<string>> m_Associators;
        public string ID { get { return GetKey(); } }

        public static T CreateObject<T>(WmiContext context, ManagementObject wmiObject)
            where T : WmiObject, new()
        {
            var r = new T();
            r.m_Context = context;
            r.m_wmiObject = wmiObject;
            r.OnLoadProperties();
            r.OnCreated();
            return r;
        }

        public WmiContext CreationContext { get { return m_Context; } }

        #region Get
        protected T Get<T>(ManagementBaseObject Object, string Property)
        {
            return (T)Get(Object, typeof(T), Property);
        }

        protected T Get<T>(string Property)
        {
            return (T)Get(typeof(T), Property);
        }

        protected object Get(Type type, string Property)
        {
            var o = Get(Property);
            if (type.IsEnum)
                return Enum.ToObject(type, o);
            return o;
        }

        protected object Get(ManagementBaseObject Object, Type type, string Property)
        {
            var o = Get(Object, Property);
            if (type.IsEnum)
                return Enum.ToObject(type, o);
            return o;
        }

        protected object Get(string Property)
        {
            return Get(m_wmiObject, Property);
        }

        protected object Get(ManagementBaseObject Object, string Property)
        {
            return Object[Property];
        }
        #endregion

        protected object Call(string MethodName, params object[] parameters)
        {
            return m_wmiObject.InvokeMethod(MethodName, parameters);
        }

        protected virtual string GetKey()
        {
            var a = GetKeyPropertyName();
            return a == null ? Get<string>("Name") : Get<string>(a);
        }

        public string GetKeyPropertyName()
        {
            return GetKeyPropertyName(GetType());
        }

        public static string GetKeyPropertyName(Type type)
        {
            var a = type.GetCustomAttribute<WmiClassAttribute>();
            return a == null ? null : a.KeyProperty;
        }

        public static IEnumerable<string> GetWmiPropertyNames(Type type)
        {
            var r = new List<string>();
            if (!type.IsSubclassOf(typeof(WmiObject)))
                return null;

            r.Add(GetKeyPropertyName(type));
            foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var a = pi.GetCustomAttribute<WmiPropertyAttribute>();
                if (a != null)
                    if (!r.Contains(a.Property ?? pi.Name))
                        r.Add(a.Property ?? pi.Name);
            }
            return r;
        }

        public string GetClassName()
        {
            return GetClassName(GetType());
        }

        public static string GetClassName(Type type)
        {
            var a = type.GetCustomAttribute<WmiClassAttribute>();
            return a == null ? null : a.ClassName;
        }

        protected virtual void OnCreated() { }

        protected virtual void OnLoadProperties()
        {
            LoadProperties();
        }

        private void LoadProperties()
        {
            var t = this.GetType();
            foreach (var pi in t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
            {
                var a = pi.GetCustomAttribute<WmiPropertyAttribute>();
                if (a != null)
                    pi.SetValue(this, Get(pi.PropertyType, a.Property ?? pi.Name));
            }
        }

        public override string ToString()
        {
            return ID.ToString();
        }

        public static string GetWmiQueryValue(object value)
        {
            if (value is string)
                return value.ToString().Replace(@"\", @"\\");
            else
                return value.ToString();
        }
    }

    internal abstract class WmiObject<TObject> : WmiObject
        where TObject : WmiObject<TObject>
    {
        private static Dictionary<string, TObject> m_Cache;
        private static Dictionary<string, TObject> Cache { get { if (m_Cache == null) CreateCache(); return m_Cache; } }

        [Obsolete("Use WmiContext.Instance<T>() instead")]
        public static IEnumerable<TObject> AsEnumerable()
        {
            Reset();
            LoadAllObjects();
            return Cache.Values;
        }

        [Obsolete("Use WmiContext.Instance<T>() instead")]
        public static void FillCache()
        {
            Reset();
            LoadAllObjects();
        }

        private static void CreateCache()
        {
            m_Cache = new Dictionary<string, TObject>();
        }

        private static void LoadAllObjects()
        {
            LoadObjects(null);
        }

        private static void LoadObject(object ID)
        {
            var a = typeof(TObject).GetCustomAttribute<WmiClassAttribute>();
            if (a == null)
                throw new ArgumentNullException("Attribute missing");
            LoadObjects(string.Format("{0}=\"{1}\"", a.KeyProperty, GetWmiQueryValue(ID)));
        }

        private static void LoadObjects(string Condition)
        {
            var a = typeof(TObject).GetCustomAttribute<WmiClassAttribute>();
            if (a == null)
                throw new ArgumentNullException("Attribute missing");

            string q = string.Format("SELECT * FROM {0}", a.ClassName);
            if (!string.IsNullOrEmpty(Condition))
                q += " WHERE " + Condition;

            var os = new ManagementObjectSearcher();
            os.Query = new ObjectQuery(q);
            os.Options.ReturnImmediately = false;
            os.Options.DirectRead = true;

            foreach (ManagementObject o in os.Get().Cast<ManagementObject>())
            {
                TObject x = (TObject)Activator.CreateInstance(typeof(TObject));
                x.m_wmiObject = o;
                x.OnLoadProperties();
                x.OnCreated();
                if (Cache.ContainsKey(x.ID))
                    Cache.Remove(x.ID);
                Cache.Add(x.ID, x);
            }
        }

        protected void AddAssociators<T>()
            where T : WmiObject<T>, new()
        {
            if (m_Associators == null)
                m_Associators = new Dictionary<Type, IEnumerable<string>>();

            var l = new List<string>();
            l.AddRange(this.Associators<T>().Select(z => z.ID));

            m_Associators.Add(typeof(T), l);
        }

        public IEnumerable<T> GetAssociators<T>()
            where T : WmiObject<T>, new()
        {
            if (m_Associators == null || !m_Associators.ContainsKey(typeof(T)))
                AddAssociators<T>();
            return m_Associators[typeof(T)].Select(z => WmiObject<T>.Find(z)).Where(z => z != null);
        }

        public static TObject Find(string Key)
        {
            TObject o;
            if (!Cache.ContainsKey(Key))
                LoadObject(Key);
            Cache.TryGetValue(Key, out o);
            return o;
        }

        protected override void OnCreated()
        {
            base.OnCreated();
            Cache.Remove(this.ID);
            Cache.Add(this.ID, (TObject)this);
        }
        public static void Reset()
        {
            m_Cache = null;
        }
    }
}
