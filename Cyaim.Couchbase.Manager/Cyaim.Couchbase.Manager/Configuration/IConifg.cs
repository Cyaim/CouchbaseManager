using System;
using System.Collections.Generic;
using System.Text;
using c = Couchbase;

namespace Cyaim.Couchbase.Manager.Configuration
{
    public interface IConifg
    {
        /// <summary>
        /// Get Cluster object by config path
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Couchebase Cluster</returns>
        c.Cluster LoadConfigFile(string path);

        /// <summary>
        /// Get Cluster object by config
        /// </summary>
        /// <param name="config"></param>
        /// <returns>Couchebase Cluster</returns>
        c.Cluster LoadConfig(string config);


        /// <summary>
        /// Create CouchbaseManager object by config file
        /// </summary>
        /// <param name="path">Config json path</param>
        /// <returns>CouchbaseManager</returns>
        CouchbaseManager CreateManagerFile(string path);

        /// <summary>
        /// Create CouchbaseManager object by config string
        /// </summary>
        /// <param name="config">Config json</param>
        /// <returns>CouchbaseManager</returns>
        CouchbaseManager CreateManager(string config);

        //void WriteConifFile(string path);

        //string GetConfig();
    }
}
