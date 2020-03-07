using RPGDatabase.Runtime.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGDatabase.Runtime.Core
{
    public class DatabaseManager
    {
        protected Dictionary<Type, DatabaseEntry> _entries;

        public DatabaseManager()
        {
            _entries = new Dictionary<Type, DatabaseEntry>();
        }

        public void Load()
        {
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

        public int TotalEntries { get { return _entries.Count; } private set { } }
    }
}