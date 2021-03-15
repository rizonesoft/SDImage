using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace OSX.WmiLib
{
    public class IOWrapper
    {
        //
        // CreateFile constants
        //

        const uint FILE_DEVICE_DISK = 0x00000007;
        const uint FILE_DEVICE_FILE_SYSTEM = 0x00000009;
        const uint FILE_DEVICE_MASS_STORAGE = 0x0000002d;
        const uint METHOD_BUFFERED = 0;
        const uint FILE_ANY_ACCESS = 0;
        const uint FILE_READ_ACCESS = (0x0001);
        const uint FILE_WRITE_ACCESS = (0x0002);
        const uint IOCTL_DISK_BASE = FILE_DEVICE_DISK;
        const uint IOCTL_STORAGE_BASE = FILE_DEVICE_MASS_STORAGE;

        private static uint FSCTL_LOCK_VOLUME = CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 6, METHOD_BUFFERED, FILE_ANY_ACCESS);
        private static uint FSCTL_UNLOCK_VOLUME = CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 7, METHOD_BUFFERED, FILE_ANY_ACCESS);
        private static uint FSCTL_DISMOUNT_VOLUME = CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 8, METHOD_BUFFERED, FILE_ANY_ACCESS);
        private static uint IOCTL_DISK_GET_PARTITION_INFO_EX = CTL_CODE(IOCTL_DISK_BASE, 0x0012, METHOD_BUFFERED, FILE_ANY_ACCESS);
        private static uint IOCTL_DISK_SET_PARTITION_INFO_EX = CTL_CODE(IOCTL_DISK_BASE, 0x0013, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS);
        private static uint IOCTL_DISK_GET_DRIVE_LAYOUT_EX = CTL_CODE(IOCTL_DISK_BASE, 0x0014, METHOD_BUFFERED, FILE_ANY_ACCESS);
        private static uint IOCTL_DISK_SET_DRIVE_LAYOUT_EX = CTL_CODE(IOCTL_DISK_BASE, 0x0015, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS);
        private static uint IOCTL_DISK_CREATE_DISK = CTL_CODE(IOCTL_DISK_BASE, 0x0016, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS);
        private static uint IOCTL_DISK_GET_LENGTH_INFO = CTL_CODE(IOCTL_DISK_BASE, 0x0017, METHOD_BUFFERED, FILE_READ_ACCESS);
        private static uint IOCTL_DISK_GET_DRIVE_GEOMETRY_EX = CTL_CODE(IOCTL_DISK_BASE, 0x0028, METHOD_BUFFERED, FILE_ANY_ACCESS);
        private static uint IOCTL_DISK_UPDATE_PROPERTIES = CTL_CODE(IOCTL_DISK_BASE, 0x0050, METHOD_BUFFERED, FILE_ANY_ACCESS);
        private static uint IOCTL_STORAGE_LOAD_MEDIA = CTL_CODE(IOCTL_STORAGE_BASE, 0x0203, METHOD_BUFFERED, FILE_READ_ACCESS);

        private static uint CTL_CODE(uint DeviceType, uint Function, uint Method, uint Access)
        {
            return ((DeviceType) << 16) | ((Access) << 14) | ((Function) << 2) | (Method);
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern SafeFileHandle CreateFile(
           string lpFileName,
           [MarshalAs(UnmanagedType.U4)] FileAccess DesiredAccess,
           [MarshalAs(UnmanagedType.U4)] FileShare ShareMode,
           IntPtr lpSecurityAttributes,
           [MarshalAs(UnmanagedType.U4)] FileMode CreationDisposition,
           [MarshalAs(UnmanagedType.U4)] FileAttributes FlagsAndAttributes,
           IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool DeviceIoControl(
            IntPtr hDevice,
            uint dwIoControlCode,
            IntPtr lpInBuffer,
            uint nInBufferSize,
            [Out] IntPtr lpOutBuffer,
            uint nOutBufferSize,
            ref uint lpBytesReturned,
            IntPtr lpOverlapped);

        public enum PARTITION_STYLE : int
        {
            MBR = 0,
            GPT = 1,
            RAW = 2
        }

        public enum Partition : byte
        {
            ENTRY_UNUSED = 0,
            FAT_12 = 1,
            XENIX_1 = 2,
            XENIX_2 = 3,
            FAT_16 = 4,
            EXTENDED = 5,
            HUGE = 6,
            IFS = 7,
            OS2BOOTMGR = 0xa,
            FAT32 = 0xb,
            FAT32_XINT13 = 0xc,
            XINT13 = 0xe,
            XINT13_EXTENDED = 0xf,
            PREP = 0x41,
            LDM = 0x42,
            UNIX = 0x63
        }

        // Needs to be explicit to do the union.
        [StructLayout(LayoutKind.Explicit)]
        public struct DRIVE_LAYOUT_INFORMATION_EX
        {
            [FieldOffset(0)]
            public PARTITION_STYLE PartitionStyle;
            [FieldOffset(4)]
            public int PartitionCount;
            [FieldOffset(8)]
            public DRIVE_LAYOUT_INFORMATION_MBR Mbr;
            [FieldOffset(8)]
            public DRIVE_LAYOUT_INFORMATION_GPT Gpt;
            // Forget partition entry, we can't marshal it directly
            // as we don't know how big it is.
            [FieldOffset(48)]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public PARTITION_INFORMATION_EX[] PartitionEntry;
        }

        public struct DRIVE_LAYOUT_INFORMATION_MBR
        {
            public uint Signature;
        }

        // Sequential ensures the fields are laid out in memory
        // in the same order as we write them here. Without it,
        // the runtime can arrange them however it likes, and the
        // type may no longer be blittable to the C type.
        [StructLayout(LayoutKind.Sequential)]
        public struct DRIVE_LAYOUT_INFORMATION_GPT
        {
            public Guid DiskId;
            // C LARGE_INTEGER is 64 bit
            public long StartingUsableOffset;
            public long UsableLength;
            // C ULONG is 32 bit
            public uint MaxPartitionCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PARTITION_INFORMATION_MBR
        {
            public byte PartitionType;
            [MarshalAs(UnmanagedType.U1)]
            public bool BootIndicator;
            [MarshalAs(UnmanagedType.U1)]
            public bool RecognizedPartition;
            public UInt32 HiddenSectors;

            // helper method - is the hi bit valid - if so IsNTFT has meaning.
            public bool IsValidNTFT()
            {
                return (PartitionType & 0xc0) == 0xc0;
            }

            // is this NTFT - i.e. an NTFT raid or mirror.
            public bool IsNTFT()
            {
                return (PartitionType & 0x80) == 0x80;
            }

            // the actual partition type.
            public Partition GetPartition()
            {
                const byte mask = 0x3f;
                return (Partition)(PartitionType & mask);
            }
        }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        public struct PARTITION_INFORMATION_GPT
        {
            // Strangely, this works as sequential if you build x86,
            // But for x64 you must use explicit.
            [FieldOffset(0)]
            public Guid PartitionType;
            [FieldOffset(16)]
            public Guid PartitionId;
            [FieldOffset(32)]
            //DWord64
            public ulong Attributes;
            [FieldOffset(40)]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
            public string Name;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct PARTITION_INFORMATION_EX
        {
            [FieldOffset(0)]
            public PARTITION_STYLE PartitionStyle;
            [FieldOffset(8)]
            public long StartingOffset;
            [FieldOffset(16)]
            public long PartitionLength;
            [FieldOffset(24)]
            public int PartitionNumber;
            [FieldOffset(28)]
            [MarshalAs(UnmanagedType.U1)]
            public bool RewritePartition;
            [FieldOffset(32)]
            public PARTITION_INFORMATION_MBR Mbr;
            [FieldOffset(32)]
            public PARTITION_INFORMATION_GPT Gpt;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CREATE_DISK_GPT
        {
            public Guid DiskId;
            public uint MaxPartitionCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CREATE_DISK_MBR
        {
            public uint Signature;     
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CREATE_DISK
        {
            [FieldOffset(0)]
            public PARTITION_STYLE PartitionStyle;
            [FieldOffset(4)]
            public CREATE_DISK_MBR Mbr;
            [FieldOffset(4)]
            public CREATE_DISK_GPT Gpt;
        }

        public static void LockVolume(SafeHandle handle)
        {
            uint bytesReturned = 0;
            var b = DeviceIoControl(handle.DangerousGetHandle(), FSCTL_LOCK_VOLUME, IntPtr.Zero, 0, IntPtr.Zero, 0, ref  bytesReturned, IntPtr.Zero);
            if (!b)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }

        public static void UnlockVolume(SafeHandle handle)
        {
            uint bytesReturned = 0;
            var b = DeviceIoControl(handle.DangerousGetHandle(), FSCTL_UNLOCK_VOLUME, IntPtr.Zero, 0, IntPtr.Zero, 0, ref  bytesReturned, IntPtr.Zero);
            if (!b)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }

        public static void DismountVolume(SafeHandle handle)
        {
            uint bytesReturned = 0;
            var b = DeviceIoControl(handle.DangerousGetHandle(), FSCTL_DISMOUNT_VOLUME, IntPtr.Zero, 0, IntPtr.Zero, 0, ref  bytesReturned, IntPtr.Zero);
            if (!b)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }

        public static long GetLength(SafeHandle handle)
        {
            var hResult = Marshal.AllocHGlobal(sizeof(ulong));
            uint bytesReturned = 0;
            long r = 0;
            try
            {
                var b = DeviceIoControl(handle.DangerousGetHandle(), IOCTL_DISK_GET_LENGTH_INFO, IntPtr.Zero, 0, hResult, sizeof(long), ref bytesReturned, IntPtr.Zero);
                if (!b)
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                r = Marshal.ReadInt64(hResult);
            }
            finally
            {
                Marshal.FreeHGlobal(hResult);
            }
            return r;
        }

        public static FileStream GetFileStream(string filename, FileMode mode, FileAccess access, FileShare share)
        {
            SafeFileHandle h = CreateFile(filename, access, share, IntPtr.Zero, mode, FileAttributes.Normal, IntPtr.Zero);
            if (h.IsInvalid)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return new FileStream(h, access);
        }

        public static DRIVE_LAYOUT_INFORMATION_EX DiskGetDriveLayoutEx(SafeHandle handle)
        {
            var bufSize = Marshal.SizeOf(typeof(DRIVE_LAYOUT_INFORMATION_EX));
            var buf = Marshal.AllocHGlobal(bufSize);
            uint bytesReturned = 0;
            var b = DeviceIoControl(handle.DangerousGetHandle(), IOCTL_DISK_GET_DRIVE_LAYOUT_EX, IntPtr.Zero, 0, buf, (uint)bufSize, ref bytesReturned, IntPtr.Zero);
            if (!b)
            {
                Marshal.FreeHGlobal(buf);
                throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
            }
            else
            {
                var r = (DRIVE_LAYOUT_INFORMATION_EX)Marshal.PtrToStructure(buf, typeof(DRIVE_LAYOUT_INFORMATION_EX));
                Marshal.FreeHGlobal(buf);
                return r;
            }
        }

        public static void DiskSetDriveLayoutEx(SafeHandle handle, DRIVE_LAYOUT_INFORMATION_EX layout)
        {
            var bufSize = Marshal.SizeOf(layout);
            var buf = Marshal.AllocHGlobal(bufSize);
            uint bytesReturned = 0;
            Marshal.StructureToPtr(layout, buf, false);
            var b = DeviceIoControl(handle.DangerousGetHandle(), IOCTL_DISK_SET_DRIVE_LAYOUT_EX, buf, (uint)bufSize, IntPtr.Zero, 0, ref bytesReturned, IntPtr.Zero);
            Marshal.FreeHGlobal(buf);
            if (!b)
                throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
        }

        public static void DiskCreateDiskMBR(SafeHandle handle, uint Signature)
        {
            CREATE_DISK r = new CREATE_DISK();
            r.PartitionStyle = PARTITION_STYLE.MBR;
            r.Mbr.Signature = Signature;
            var bufSize = Marshal.SizeOf(r);
            var buf = Marshal.AllocHGlobal(bufSize);
            Marshal.StructureToPtr(r, buf, false);
            uint bytesReturned = 0;
            var b = DeviceIoControl(handle.DangerousGetHandle(), IOCTL_DISK_CREATE_DISK, buf, (uint)bufSize, IntPtr.Zero, 0, ref bytesReturned, IntPtr.Zero);
            Marshal.FreeHGlobal(buf);
            if (!b)
                throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());

        }

        public static void DiskUpdateProperties(SafeHandle handle)
        {
            uint bytesReturned = 0;
            var b = DeviceIoControl(handle.DangerousGetHandle(), IOCTL_DISK_UPDATE_PROPERTIES, IntPtr.Zero, 0, IntPtr.Zero, 0, ref bytesReturned, IntPtr.Zero);
            if (!b)
                throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
        }

        public static void StorageLoadMedia(SafeHandle handle)
        {
            uint bytesReturned = 0;
            var b = DeviceIoControl(handle.DangerousGetHandle(), IOCTL_STORAGE_LOAD_MEDIA, IntPtr.Zero, 0, IntPtr.Zero, 0, ref bytesReturned, IntPtr.Zero);
            if (!b)
                throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
    
    }

}
