using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSX.WmiLib
{
    [WmiClass("Win32_DiskPartition")]
    internal class DiskPartition : WmiObject<DiskPartition>
    {
        [WmiProperty]
        public ulong BlockSize { get; private set; }
        [WmiProperty]
        public ulong NumberOfBlocks { get; private set; }
        [WmiProperty]
        public ulong Size { get; private set; }
        [WmiProperty]
        public string Type { get; private set; }
        [WmiProperty]
        public uint DiskIndex { get; private set; }
        [WmiProperty]
        public uint Index { get; private set; }
        [WmiProperty]
        public string Description { get; private set; }
        [WmiProperty]
        public ulong StartingOffset { get; private set; }
        [WmiProperty]
        public bool PrimaryPartition { get; private set; }
        [WmiProperty]
        public bool Bootable { get; private set; }
        [WmiProperty]
        public bool BootPartition { get; private set; }

        public IEnumerable<LogicalDisk> LogicalDisks { get { return GetAssociators<LogicalDisk>(); } }

    }
}
