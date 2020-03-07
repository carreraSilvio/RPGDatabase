using UnityEngine;

namespace RPGDatabase.Runtime.Core
{
    public class DatabaseUtils
    {
        private static DatabaseConfigData config;
        public static DatabaseConfigData Config { get { if (config == null) Init(); return config; } }

        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            config = DatabaseLoader.LoadDatabaseConfig();

            if(config == null)
            {
                Debug.LogWarning("Database not created properly. Make sure to open Tools/Database to create it.");
            }
        }

    }

}
