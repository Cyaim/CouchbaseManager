using Couchbase.Configuration.Client;
using c = Couchbase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Couchbase.Authentication;
using Cyaim.Couchbase.Manager.Exception;
using System.IO;
using Cyaim.Couchbase.Manager.Type;

namespace Cyaim.Couchbase.Manager.Configuration
{
    public class Config : IConifg
    {
        private const string configTemp = @"{
    ""Cluster"": {
        ""UserName"": """",
        ""Password"": """",
		""UseSSL"": """",
        ""SSLPort"": 0,
        ""Servers"": [
            {
                ""ServerName"": """",
                ""Ip"": """",
                ""Port"": """",
            },
            {
                ""ServerName"": """",
                ""Ip"": """",
                ""Port"": """",
            }
        ]
    },
	""Buckets"":[
		{
			""ServerName"":"""",
			""BucketName"":"""",
		},
		{
			""ServerName"":"""",
			""BucketName"":"""",
		}
	]
}";
        /// <summary>
        /// Create CouchbaseManager object by config string
        /// </summary>
        /// <param name="config">Config json</param>
        /// <returns></returns>
        public CouchbaseManager CreateManager(string config)
        {
            var r = LoadCouchbaseConfig(config);
            var bucket = new Type.OneToManyList<string, string>();
            foreach (var item in r.couchbaseConfig.Buckets)
            {
                bucket.Add(item.ServerName, item.BucketName);
            }

            CouchbaseManager couchbaseManager = new CouchbaseManager()
            {
                Cluster = r.cluster,
                ConfigData = config,
                Buckets = bucket,
            };

            return couchbaseManager;
        }

        /// <summary>
        /// Create CouchbaseManager object by CouchbaseConfig
        /// </summary>
        /// <param name="config">Config json</param>
        /// <returns></returns>
        public CouchbaseManager CreateManager(CouchbaseConfig config)
        {
            var r = LoadConfig(config);
            var bucket = new Type.OneToManyList<string, string>();
            foreach (var item in config.Buckets)
            {
                bucket.Add(item.ServerName, item.BucketName);
            }

            CouchbaseManager couchbaseManager = new CouchbaseManager()
            {
                Cluster = r,
                ConfigData = JsonConvert.SerializeObject(config),
                Buckets = bucket,
            };

            return couchbaseManager;
        }

        /// <summary>
        /// Create CouchbaseManager object by config file
        /// </summary>
        /// <param name="path">Config json path</param>
        /// <returns></returns>
        public CouchbaseManager CreateManagerFile(string path)
        {
            string config = null;
            try
            {
                config = File.ReadAllText(path);
            }
            catch (System.Exception)
            {
                throw;
            }

            return CreateManager(config);
        }

        /// <summary>
        /// Get Cluster object by config
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public c.Cluster LoadConfig(string config)
        {
            if (config == null)
            {
                throw new System.NullReferenceException("传入的配置信息为空。");
            }
            CouchbaseConfig r = null;
            try
            {
                r = GetCouchbaseConfig(config);
            }
            catch (System.Exception)
            {
                throw new ConfigurationException("读取配置文件引发此异常，检查配置信息是否正确。");
            }

            if (r.Cluster.UserName == null || r.Cluster.Password == null)
            {
                throw new ClusterAuthenticatorException("连接集群时引发此异常，用户名、密码验证失败。");
            }

            var serverUrl = new List<Uri>();
            try
            {
                foreach (var item in r.Cluster.Servers)
                {
                    serverUrl.Add(new Uri($"{item.Ip}:{item.Port}"));
                }
            }
            catch (System.Exception)
            {
                throw new ClusterAuthenticatorException(@"读取配置文件引发此异常，检查""Servers""节点配置信息。");
            }


            c.Cluster cluster = null;

            try
            {
                cluster = new c.Cluster(new ClientConfiguration
                {
                    Servers = serverUrl,
                    UseSsl = r.Cluster.UseSSL,
                    SslPort = r.Cluster.SSLPort
                });
            }
            catch (System.Exception)
            {
                throw new ClusterAuthenticatorException(@"读取配置文件引发此异常，检查""Cluster""节点配置信息。");
            }


            var authenticator = new PasswordAuthenticator(r.Cluster.UserName, r.Cluster.Password);
            cluster.Authenticate(authenticator);
            return cluster;
        }

        /// <summary>
        /// Get Cluster object by config path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public c.Cluster LoadConfigFile(string path)
        {
            string config = null;
            try
            {
                config = File.ReadAllText(path);
            }
            catch (System.Exception)
            {
                throw;
            }

            return LoadConfig(config);
        }

        /// <summary>
        /// Get Cluster object by CouchbaseConfig
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public c.Cluster LoadConfig(CouchbaseConfig config)
        {
            if (config == null)
            {
                throw new System.NullReferenceException("传入的配置信息为空。");
            }
            CouchbaseConfig r = config;

            if (r.Cluster.UserName == null || r.Cluster.Password == null)
            {
                throw new ClusterAuthenticatorException("连接集群时引发此异常，用户名、密码验证失败。");
            }

            var serverUrl = new List<Uri>();
            try
            {
                foreach (var item in r.Cluster.Servers)
                {
                    serverUrl.Add(new Uri($"{item.Ip}:{item.Port}"));
                }
            }
            catch (System.Exception)
            {
                throw new ClusterAuthenticatorException(@"读取配置文件引发此异常，检查""Servers""节点配置信息。");
            }


            c.Cluster cluster = null;

            try
            {
                cluster = new c.Cluster(new ClientConfiguration
                {
                    Servers = serverUrl,
                    UseSsl = r.Cluster.UseSSL,
                    SslPort = r.Cluster.SSLPort
                });
            }
            catch (System.Exception)
            {
                throw new ClusterAuthenticatorException(@"读取配置文件引发此异常，检查""Cluster""节点配置信息。");
            }


            var authenticator = new PasswordAuthenticator(r.Cluster.UserName, r.Cluster.Password);
            cluster.Authenticate(authenticator);
            return cluster;
        }

        /// <summary>
        /// Write the configuration template to disk
        /// </summary>
        /// <param name="path">Write path</param>
        public static void WriteConifFile(string path)
        {
            File.WriteAllText(path, configTemp);
        }

        /// <summary>
        /// Get configuration template string
        /// </summary>
        /// <returns></returns>
        public static string GetConfig()
        {
            return configTemp;
        }

        private CouchbaseConfig GetCouchbaseConfig(string config)
        {
            try
            {
                return JsonConvert.DeserializeObject<CouchbaseConfig>(config);
            }
            catch (System.Exception)
            {
                throw new ConfigurationException("读取配置文件引发此异常，检查配置信息是否正确。");
            }
        }

        private (c.Cluster cluster, CouchbaseConfig couchbaseConfig) LoadCouchbaseConfig(string config)
        {
            if (config == null)
            {
                throw new System.NullReferenceException("传入的配置信息为空。");
            }
            CouchbaseConfig r = null;
            try
            {
                r = GetCouchbaseConfig(config);
            }
            catch (System.Exception)
            {
                throw new ConfigurationException("读取配置文件引发此异常，检查配置信息是否正确。");
            }

            if (r.Cluster.UserName == null || r.Cluster.Password == null)
            {
                throw new ClusterAuthenticatorException("连接集群时引发此异常，用户名、密码验证失败。");
            }

            var serverUrl = new List<Uri>();
            try
            {
                foreach (var item in r.Cluster.Servers)
                {
                    serverUrl.Add(new Uri($"{item.Ip}:{item.Port}"));
                }
            }
            catch (System.Exception)
            {
                throw new ClusterAuthenticatorException(@"读取配置文件引发此异常，检查""Servers""节点配置信息。");
            }


            c.Cluster cluster = null;

            try
            {
                cluster = new c.Cluster(new ClientConfiguration
                {
                    Servers = serverUrl,
                    UseSsl = r.Cluster.UseSSL,
                    SslPort = r.Cluster.SSLPort
                });
            }
            catch (System.Exception)
            {
                throw new ClusterAuthenticatorException(@"读取配置文件引发此异常，检查""Cluster""节点配置信息。");
            }


            var authenticator = new PasswordAuthenticator(r.Cluster.UserName, r.Cluster.Password);
            cluster.Authenticate(authenticator);
            return (cluster, r);
        }
    }
}
