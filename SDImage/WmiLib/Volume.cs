using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace OSX.WmiLib
{
    [WmiClass("Win32_Volume")]
    internal class Volume : WmiFileHandleObject<Volume>
    {
        [WmiProperty]
        public bool Automount { get; private set; }
        [WmiProperty]
        public ulong BlockSize { get; private set; }
        [WmiProperty]
        public bool BootVolume { get; private set; }
        [WmiProperty]
        public ulong Capacity { get; private set; }
        [WmiProperty]
        public string Name { get; private set; }
        [WmiProperty]
        public bool Compressed { get; private set; }
        [WmiProperty]
        public string DeviceID { get; private set; }
        [WmiProperty]
        public string DriveLetter { get; private set; }
        [WmiProperty]
        public DriveType DriveType { get; private set; }
        [WmiProperty]
        public string FileSystem { get; private set; }
        [WmiProperty]
        public ulong FreeSpace { get; private set; }
        [WmiProperty]
        public string Label { get; private set; }
        [WmiProperty]
        public uint MaximumFileNameLength { get; private set; }
        [WmiProperty]
        public bool PageFilePresent { get; private set; }
        [WmiProperty]
        public uint SerialNumber { get; private set; }

        public IEnumerable<Directory> MountPoints { get { return GetAssociators<Directory>(); } }

        //public static Volume FindName(string NameToFind)
        //{
        //    return AsEnumerable().FirstOrDefault(z => z.Name == NameToFind);
        //}

        private SafeFileHandle m_LockHandle;
        public SafeFileHandle GetLockHandle()
        {
            return m_LockHandle;
        }

        public void Lock()
        {
            if (m_LockHandle != null && m_LockHandle.IsInvalid)
                throw new InvalidOperationException("Volume already locked");

            var handle = CreateHandle(FileAccess.ReadWrite, FileShare.ReadWrite);
            IOWrapper.LockVolume(handle);
            m_LockHandle = handle;
        }

        public void Unlock()
        {
            if (m_LockHandle == null)
                throw new InvalidOperationException("Volume was not locked");
            IOWrapper.UnlockVolume(m_LockHandle);
            m_LockHandle.Close();
            m_LockHandle = null;
        }

        public void Dismount(bool Force = false, bool Permanent = false)
        {
            SafeFileHandle handle = GetLockHandle();
            if (handle != null)
                IOWrapper.DismountVolume(handle);
            else
            {
                var r = (uint)Call("Dismount", Force, Permanent);
                switch (r)
                {
                    case 1: throw new SecurityException("Access Denied");
                    case 2: throw new IOException("Volume Has Mount Points");
                    case 3: throw new IOException("Volume Does Not Support The No-Autoremount State");
                    case 4: throw new IOException("Force Option Required");
                }
            }
        }

        public void Mount()
        {
            var r = (uint)Call("Mount", null);
            switch (r)
            {
                case 1: throw new SecurityException("Access Denied");
                case 2: throw new IOException("Unknown Error");
            }
        }

        public void Format(string FileSystem = "NTFS", bool QuickFormat = false, uint ClusterSize = 0, string Label = "", bool EnableCompression = false)
        {
            if (ClusterSize == 0)
            {
                var p = m_wmiObject.GetMethodParameters("Format");
                ClusterSize = Get<uint>(p, "ClusterSize");
            }

            var r = (uint)Call("Format", FileSystem, QuickFormat, ClusterSize, Label, EnableCompression);
            switch (r)
            {
                case 1: throw new InvalidOperationException("Unsupported file system");
                case 2: throw new IOException("Incompatible media in drive");
                case 3: throw new SecurityException("Access denied");
                case 4: throw new IOException("Call canceled");
                case 5: throw new IOException("Call cancellation request too late");
                case 6: throw new IOException("Volume write protected");
                case 7: throw new IOException("Volume lock failed");
                case 8: throw new IOException("Unable to quick format");
                case 9: throw new IOException("Input/Output (I/O) error");
                case 10: throw new IOException("Invalid volume label");
                case 11: throw new IOException("No media in drive");
                case 12: throw new IOException("Volume is too small");
                case 13: throw new IOException("Volume is too large");
                case 14: throw new IOException("Volume is not mounted");
                case 15: throw new IOException("Cluster size is too small");
                case 16: throw new IOException("Cluster size is too large");
                case 17: throw new IOException("Cluster size is beyond 32 bits");
                case 18: throw new IOException("Unknown error");
            }
        }

        public override string GetFilename()
        {
            if (string.IsNullOrEmpty(DriveLetter))
                throw new NotSupportedException("Volume does not have a DriveLetter");
            return @"\\.\" + DriveLetter;
        }
    }
}
