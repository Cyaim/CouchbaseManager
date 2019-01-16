using Cyaim.Couchbase.Manager.Configuration;
using System;

namespace Cyaim.Couchbase.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            Config c = new Config();
            var cl = c.LoadConfigFile($@"C:\Users\Psyche\Desktop\CouchBase.json");

            Config c2 = new Config();
            var manag = c.CreateManagerFile($@"C:\Users\Psyche\Desktop\CouchBase.json");

            cl.OpenBucket();

            Console.ReadKey();
        }
    }
}
