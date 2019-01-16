using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cyaim.Couchbase.Manager.Type
{

    public class CouchbaseConfig
    {
        [JsonProperty("Cluster")]
        public Cluster Cluster { get; set; }

        [JsonProperty("Buckets")]
        public List<Bucket> Buckets { get; set; }
    }

    public class Cluster
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("UseSSL")]
        public bool UseSSL { get; set; }

        [JsonProperty("SSLPort")]
        public int SSLPort { get; set; }

        [JsonProperty("Servers")]
        public List<Server> Servers { get; set; }

    }

    public class Server
    {
        [JsonProperty("ServerName")]
        public string ServerName { get; set; }

        [JsonProperty("Ip")]
        public string Ip { get; set; }

        [JsonProperty("Port")]
        public string Port { get; set; }
    }

    public class Bucket
    {
        [JsonProperty("ServerName")]
        public string ServerName { get; set; }

        [JsonProperty("BucketName")]
        public string BucketName { get; set; }
    }
}
