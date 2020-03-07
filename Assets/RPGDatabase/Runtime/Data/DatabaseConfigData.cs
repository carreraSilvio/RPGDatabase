using UnityEngine;

namespace BrightLib.RPGDatabase.Runtime
{
    public class DatabaseConfigData : ScriptableObject
    {
        [SerializeField, HideInInspector]
        private int _entryUniqueId = 200;

        public int FetchUniqueId()
        {
            return _entryUniqueId++;
        }
    }
}