using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using OSX.WmiLib;

namespace SDImager
{
    //internal class VolumeInfo : IDisposable
    //{
    //    public string VolumeID;
    //    public string PartitionID;
    //    public string PhysicalDriveID;
    //    public long PhysicalDriveSize;
    //    public DriveInfo DriveInfo;
    //    public string InterfaceType;
    //    public string Model;

    //    FileStream hVolume;
    //    FileStream hPhysicalDrive;
    //    ManualResetEvent hWait;

    //    public override string ToString()
    //    {
    //        if (DriveInfo == null)
    //            return string.Format("{0} [{1}: {2:N0} MB]", PhysicalDriveID, InterfaceType, PhysicalDriveSize / (1024 * 1024));
    //        else
    //            try
    //            {
    //                return string.Format(@"{0}\ [{1}, {2}: {3:N0} MB]", VolumeID, DriveInfo.DriveFormat, InterfaceType, PhysicalDriveSize / (1024 * 1024));
    //            }
    //            catch
    //            {
    //                return string.Format(@"{0}\ [not ready]", VolumeID);
    //            }
    //    }

    //    public void CloseHandles()
    //    {
    //        if (hVolume != null)
    //        {
    //            hVolume.Dispose();
    //            hVolume = null;
    //        }
    //        if (hPhysicalDrive != null)
    //        {
    //            hPhysicalDrive.Dispose();
    //            hPhysicalDrive = null;
    //        }
    //    }

    //    public bool LockVolume()
    //    {
    //        if (DriveInfo != null)
    //        {
    //            GetVolumeStream();
    //            IOWrapper.LockVolume(hVolume.SafeFileHandle);
    //        }
    //        return true;
    //    }

    //    public bool UnlockVolume()
    //    {
    //        if (DriveInfo != null)
    //        {
    //            GetVolumeStream();
    //            IOWrapper.UnlockVolume(hVolume.SafeFileHandle);
    //        }
    //        return true;
    //    }

    //    public bool DismountVolume()
    //    {
    //        if (DriveInfo != null)
    //        {
    //            GetVolumeStream();
    //            IOWrapper.DismountVolume(hVolume.SafeFileHandle);
    //        }
    //        return true;
    //    }

    //    public FileStream GetVolumeStream()
    //    {
    //        if (DriveInfo == null) return null;
    //        if (hVolume != null) return hVolume;
    //        hVolume = IOWrapper.GetFileStream(string.Format(@"\\.\{0}", VolumeID), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
    //        return hVolume;
    //    }

    //    public FileStream GetPhysicalDriveStream(FileAccess access)
    //    {
    //        if (hPhysicalDrive != null) return hPhysicalDrive;
    //        hPhysicalDrive = IOWrapper.GetFileStream(PhysicalDriveID, FileMode.Open, access, FileShare.ReadWrite);
    //        return hPhysicalDrive;
    //    }

    //    private ManagementObject GetWMIVolume()
    //    {
    //        //return new ManagementObject(string.Format(@"Win32_Volume.Name='{0}\\'", VolumeID));
    //        foreach (var o in new ManagementObjectSearcher("SELECT * FROM Win32_Volume WHERE DriveLetter='" + VolumeID + "'").Get())
    //            return (ManagementObject)o;
    //        return null;
    //    }

    //    public void Format(ProgressEventHandler progressHandler, CancellationToken token)
    //    {
    //        if (DriveInfo == null)
    //            throw new NotSupportedException();

    //        var mo = GetWMIVolume();
    //        object r;
    //        r = mo.InvokeMethod("Dismount", new object[] { true, false });
    //        var param = mo.GetMethodParameters("Format");
    //        param["FileSystem"] = "FAT32";
    //        param["QuickFormat"] = true;
    //        param["Label"] = "Empty";
    //        var ob = new ManagementOperationObserver();
    //        //var tcs = new TaskCompletionSource<void>();
    //        ob.Progress += progressHandler;
    //        ob.Completed += OnCompleted;
    //        hWait = new ManualResetEvent(false);
    //        mo.InvokeMethod(ob, "Format", param, null);
    //        try
    //        {
    //            Task.Run(() => hWait.WaitOne()).Wait(token);
    //        }
    //        catch (OperationCanceledException)
    //        {
    //            ob.Cancel();
    //        }
    //        hWait.Dispose();
    //        hWait = null;
    //        r = mo.InvokeMethod("Mount", null);
    //    }

    //    private void OnCompleted(object sender, CompletedEventArgs e)
    //    {
    //        hWait.Set();
    //    }

    //    public void Dispose()
    //    {
    //        CloseHandles();
    //    }
    //}

    internal static class DriveTools
    {
        private static List<ManagementEventWatcher> mew;
        private static EventArrivedEventHandler NotifyHandler;

        public static IEnumerable<DiskDrive> GetRemovableDiskDrives()
        {
            return new WmiContext().DiskDrives.Where(z => z.MediaType.Contains("removable"));
        }

        public static void StartDriveChangeNotification(EventArrivedEventHandler handler)
        {
            if (mew != null)
                throw new Exception("Already started");

            NotifyHandler = handler;
            mew = new List<ManagementEventWatcher>();
            mew.Add(new ManagementEventWatcher("SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_DiskDrive'"));
            mew.Add(new ManagementEventWatcher("SELECT * FROM __InstanceDeletionEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_DiskDrive'"));
            mew.Add(new ManagementEventWatcher("SELECT * FROM __InstanceModificationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_DiskDrive'"));

            foreach (var m in mew)
            {
                m.EventArrived += EventArrived;
                m.Start();
            }
        }

        public static void StopDriveChangeNotification()
        {
            if (mew != null)
                foreach (var m in mew)
                {
                    m.Stop();
                    m.Dispose();
                }
        }

        private static void EventArrived(object sender, EventArrivedEventArgs e)
        {
            if (NotifyHandler != null)
                NotifyHandler(NotifyHandler.Target, e);
        }
    }
}
