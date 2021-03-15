using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OSX.WmiLib
{
    internal static class WmiInfo
    {
        public static string GetKeyProperty(string wmiClass)
        {
            var c = new ManagementClass(wmiClass);
            var p = c.Properties;
            foreach (var prop in p)
                foreach (var q in prop.Qualifiers)
                    if (q.Name == "key")
                        return prop.Name;
            return null;
        }

        public static void Reset()
        {
            Type[] types;
            try
            {
                types = Assembly.GetExecutingAssembly().GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                types = e.Types;
            }

            foreach (Type t in types.Where(z => z != null && !z.IsGenericTypeDefinition && z.IsSubclassOf(typeof(WmiObject))))
            {
                var mi = t.GetMethod("Reset", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
                if (mi != null)
                    mi.Invoke(null, null);
            }
        }

        public static void LoadDiskInfo()
        {
            Reset();
        }
    }
}
