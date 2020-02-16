using UnityEngine;

public class DatabaseLoader
{
    private static readonly string _kDefaultFolder = "Database";

    public static DatabaseConfigData LoadDatabaseConfig()
    {
        var databaseConfig = Resources.LoadAll<DatabaseConfigData>(_kDefaultFolder)[0];
       
        return databaseConfig;
    }

    public static DatabaseEntry[] LoadDatabaseAssets()
    {
        var databaseEntries = Resources.LoadAll<DatabaseEntry>(_kDefaultFolder);

        return databaseEntries;
    }
}
