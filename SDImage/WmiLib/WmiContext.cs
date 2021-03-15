using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace OSX.WmiLib
{
    internal class WmiContext
    {
        private ManagementScope m_Scope = null;
        private WmiQueryBuilder m_QueryBuilder;
        private bool m_FetchAllFields = true;

        public IWmiQueryable<T> Instances<T>()
            where T : WmiObject<T>, new()
        {
            return new WmiQuery<T>(this);
        }

        public bool FetchAllFields { get { return m_FetchAllFields; } set { m_FetchAllFields = value; } }

        public IEnumerator<TResult> Execute<TResult>(IWmiQueryable query)
            where TResult: WmiObject<TResult>, new()
        {
            if (m_QueryBuilder == null)
                m_QueryBuilder = new WmiQueryBuilder(this);

            var querystring = m_QueryBuilder.Translate(query, m_FetchAllFields);
            var objQuery = new ObjectQuery(querystring);
            var wmiSearcher = new ManagementObjectSearcher(m_Scope, objQuery);
            Debug.WriteLine(querystring);

            foreach (ManagementObject o in wmiSearcher.Get().Cast<ManagementObject>())
                yield return WmiObject.CreateObject<TResult>(this, o);
            wmiSearcher.Dispose();
        }

        public IWmiQueryable<DiskDrive> DiskDrives { get { return Instances<DiskDrive>(); } }
        public IWmiQueryable<DiskPartition> DiskPartitions { get { return Instances<DiskPartition>(); } }
        public IWmiQueryable<LogicalDisk> LogicalDisks { get { return Instances<LogicalDisk>(); } }
        public IWmiQueryable<Volume> Volumes { get { return Instances<Volume>(); } }
    }
}
