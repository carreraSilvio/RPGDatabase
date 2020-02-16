using UnityEngine;

public class DatabaseLoader
{
    private static readonly string _kDefaultFolder = "Database";
    public static DatabaseEntry[] LoadDatabaseAssets()
    {
        var databaseEntries = Resources.LoadAll<DatabaseEntry>(_kDefaultFolder);

        return databaseEntries;
    }
}
