using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace OSX.WmiLib
{
    [WmiClass("Win32_DiskDrive")]
    internal class DiskDrive : WmiFileHandleObject<DiskDrive>
    {
        [WmiProperty]
        public string InterfaceType { get; private set; }
        [WmiProperty]
        public string Model { get; private set; }
        [WmiProperty]
        public string MediaType { get; private set; }
        [WmiProperty]
        public uint Index { get; private set; }
        [WmiProperty]
        public string DeviceID { get; private set; }
        [WmiProperty]
        public uint BytesPerSector { get; private set; }
        [WmiProperty]
        public string Caption { get; private set; }
        [WmiProperty]
        public string Description { get; private set; }
        [WmiProperty]
        public string Manufacturer { get; private set; }
        [WmiProperty]
        public ulong Size { get; private set; }

        public IEnumerable<DiskPartition> DiskPartitions { get { return GetAssociators<DiskPartition>(); } }
        public IEnumerable<LogicalDisk> LogicalDisks { get { return DiskPartitions.SelectMany(p => p.LogicalDisks); } }
        public IEnumerable<Volume> Volumes { get { return LogicalDisks.Select(l => l.Volume); } }
        private ulong m_DriveSize;

        public ulong GetDriveSize()
        {
            if (m_DriveSize == 0)
                using (var handle = CreateHandle(FileAccess.Read))
                {
                    m_DriveSize = (ulong)IOWrapper.GetLength(handle);
                }
            return m_DriveSize;
        }

        public override string GetFilename()
        {
            return DeviceID;
        }

        public void LockVolumes()
        {
            foreach (var v in Volumes) v.Lock();
        }

        public void UnlockVolumes()
        {
            foreach (var v in Volumes) v.Unlock();
        }

        public void DismountVolumes()
        {
            foreach (var v in Volumes) v.Dismount(true);
        }

        public void MountVolumes()
        {
            foreach (var v in Volumes) v.Mount();
        }
    }
}
