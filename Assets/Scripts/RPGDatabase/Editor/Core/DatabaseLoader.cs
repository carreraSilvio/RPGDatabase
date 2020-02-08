using UnityEngine;

public static class DatabaseLoader
{
    public static DatabaseEntry[] LoadDatabaseAssets()
    {
        var databaseEntries = Resources.LoadAll<DatabaseEntry>("Database");
        if(databaseEntries.Length < 6)
        {
            DatabaseFactory.CreateDatabaseAsset<WeaponTypeDataList>();
        }

        return databaseEntries;
    }
}
