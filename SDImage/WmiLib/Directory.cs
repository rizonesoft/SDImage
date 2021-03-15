using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSX.WmiLib
{
    [WmiClass("Win32_Directory")]
    internal class Directory : WmiFileHandleObject<Directory>
    {
        [WmiProperty]
        public string Name { get; private set; }
        [WmiProperty]
        public string Drive { get; private set; }
        [WmiProperty]
        public string FileType { get; private set; }
        [WmiProperty]
        public string FSName { get; private set; }
        [WmiProperty]
        public string Path { get; private set; }

        public override string GetFilename()
        {
            return Name;
        }
    }
}
