# CouchbaseManager
<h1>Install</h1>
    Run one of the following lines of code or in nuget search Cyaim.Couchbase.Manager
    Install-Package Cyaim.Couchbase.Manager -Version 0.1.2.2
    dotnet add package Cyaim.Couchbase.Manager --version 0.1.2.2
    paket add Cyaim.Couchbase.Manager --version 0.1.2.2
<h1>Now start</h1>

    Config config = new Config();
    var manager = config.CreateManagerFile("config path");
    var bucket = manager.Cluster.OpenBucket("config bucket");
    var cluster = config.LoadConfigFile("config path");

    //Get config template
    Config.WriteConifFile("write config template path");
    var config = Config.GetConfig();


    //In Web Project
    //Startup.cs -> ConfigureServices Method
        services.AddScoped((x) =>
        {
            Cyaim.Couchbase.Manager.Configuration.Config config = new Cyaim.Couchbase.Manager.Configuration.Config();
            var couchbaseManager = new Cyaim.Couchbase.Manager.Type.CouchbaseConfig();
            Configuration.GetSection("CouchbaseConfig").Bind(couchbaseManager);

            return config.CreateManager(couchbaseManager);
        });
    //Controller constructor method
    public DemoController(Cyaim.Couchbase.Manager.CouchbaseManager couchbaseManager)
    {
         //To do Handler Code
    }
    
<h1>Nuget</h1>
    https://www.nuget.org/packages/Cyaim.Couchbase.Manager
