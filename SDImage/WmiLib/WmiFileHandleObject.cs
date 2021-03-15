using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OSX.WmiLib
{
    internal abstract class WmiFileHandleObject<T> : WmiObject<T>
        where T: WmiObject<T>
    {
        public abstract string GetFilename();
        protected SafeHandle m_LastHandle;
        
        private SafeFileHandle CreateHandle(FileAccess DesiredAccess, FileShare ShareMode, FileMode CreationDisposition, FileAttributes FlagsAndAttributes)
        {
            var handle = IOWrapper.CreateFile(GetFilename(), DesiredAccess, ShareMode, IntPtr.Zero, CreationDisposition, FlagsAndAttributes, IntPtr.Zero);
            if (handle.IsInvalid)
            {
                var i = Marshal.GetLastWin32Error();
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }
            m_LastHandle = handle;
            return handle;
        }

        public SafeHandle GetLastHandle()
        {
            return m_LastHandle;
        }

        public SafeFileHandle CreateHandle(FileAccess DesiredAccess, FileShare ShareMode)
        {
            return CreateHandle(DesiredAccess, ShareMode, FileMode.Open, FileAttributes.Normal);
        }

        public SafeFileHandle CreateHandle(FileAccess DesiredAccess)
        {
            return CreateHandle(DesiredAccess, DesiredAccess == FileAccess.Read ? FileShare.ReadWrite : FileShare.Read);
        }

        public FileStream CreateStream(FileAccess DesiredAccess)
        {
            return new FileStream(CreateHandle(DesiredAccess), DesiredAccess);
        }
    }
}
