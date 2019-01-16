using c = Couchbase;
using Cyaim.Couchbase.Manager.Type;
using System;
using System.Collections.Generic;

namespace Cyaim.Couchbase.Manager
{
    public class CouchbaseManager
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        public string ConfigData { get; set; }

        /// <summary>
        /// 集群信息
        /// </summary>
        public c.Cluster Cluster { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OneToManyList<string, string> Buckets { get; set; }
    }
}
