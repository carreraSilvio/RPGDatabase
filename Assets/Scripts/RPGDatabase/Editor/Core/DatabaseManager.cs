using System;
using System.Collections.Generic;

public class DatabaseManager
{
    //TODO Move this to data file
    public readonly int k_expStart = 0;
    public readonly int k_expEnd = 99999;
    public readonly int k_lvStart = 1;
    public readonly int k_lvEnd = 99;
    public readonly int k_hpStart = 10;
    public readonly int k_hpEnd = 999;
    public readonly int k_attrStart = 1;
    public readonly int k_attrEnd = 99;

    private Dictionary<Type, DatabaseEntry> _entries;

    public DatabaseManager()
    {
        if (_entries == null) _entries = new Dictionary<Type, DatabaseEntry>();

        DatabaseFolderHandler.ValidateAllFolders();
        var databaseEntries = DatabaseLoader.LoadDatabaseAssets();

        foreach (var dbentry in databaseEntries)
        {
            _entries.Add(dbentry.GetType(), dbentry);
        }
    }

    public T FetchEntry<T>() where T : DatabaseEntry
    {
        return (T)_entries[typeof(T)];
    }
}
