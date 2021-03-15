using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSX.WmiLib
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal class WmiClassAttribute : Attribute
    {
        public string ClassName { get; private set; }
        public string KeyProperty { get; private set; }
        private static Dictionary<string, string> m_KeyPropertyDictionary = new Dictionary<string, string>();

        public WmiClassAttribute(string ClassName) : this(ClassName, null) { }

        public WmiClassAttribute(string ClassName, string KeyProperty)
        {
            this.ClassName = ClassName;
            if (KeyProperty == null)
            {
                if (!m_KeyPropertyDictionary.ContainsKey(ClassName))
                    m_KeyPropertyDictionary.Add(ClassName, WmiInfo.GetKeyProperty(ClassName) ?? "Name");
                this.KeyProperty = m_KeyPropertyDictionary[ClassName];
            }
            else
                this.KeyProperty = KeyProperty;
        }
    }
}
