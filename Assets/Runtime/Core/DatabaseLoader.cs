using UnityEngine;

namespace BrightLib.RPGDatabase.Runtime
{
    public class DatabaseLoader
    {
        private static readonly string _kDefaultFolder = "Database";

        public static DatabaseConfigData LoadDatabaseConfig()
        {
            var databaseConfig = Resources.LoadAll<DatabaseConfigData>(_kDefaultFolder);

            if (databaseConfig != null)
                return databaseConfig[0];

            return null;
        }

        public static DatabaseEntry[] LoadDatabaseAssets()
        {
            var databaseEntries = Resources.LoadAll<DatabaseEntry>(_kDefaultFolder);

            return databaseEntries;
        }
    }
}