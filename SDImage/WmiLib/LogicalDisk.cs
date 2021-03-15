using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSX.WmiLib
{
    [WmiClass("Win32_LogicalDisk")]
    internal class LogicalDisk : WmiObject<LogicalDisk>
    {
        [WmiProperty]
        public string Description { get; private set; }
        [WmiProperty]
        public DriveType DriveType { get; private set; }
        [WmiProperty]
        public string FileSystem { get; private set; }
        [WmiProperty]
        public ulong FreeSpace { get; private set; }
        [WmiProperty]
        public uint MediaType { get; private set; }
        [WmiProperty]
        public ulong Size { get; private set; }
        [WmiProperty]
        public string VolumeName { get; private set; }

        private string m_VolumeID;

        public Volume Volume
        {
            get
            {
                if (m_VolumeID == null)
                {
                    var v = CreationContext.Instances<Volume>().Where(z => z.Name == this.ID + @"\").FirstOrDefault();
                    m_VolumeID = v.ID;
                    return v;
                }
                else
                    return Volume.Find(m_VolumeID);
            }
        }
    }
}
