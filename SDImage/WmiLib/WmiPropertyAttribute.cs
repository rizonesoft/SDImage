using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSX.WmiLib
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    internal class WmiPropertyAttribute : Attribute
    {
        public string Property { get; private set; }
        public WmiPropertyAttribute(string Property)
        {
            this.Property = Property;
        }
        public WmiPropertyAttribute() : this(null) { }
    }
}
