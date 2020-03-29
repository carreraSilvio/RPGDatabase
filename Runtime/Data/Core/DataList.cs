using System.Collections.Generic;
using UnityEngine;

namespace BrightLib.RPGDatabase.Runtime
{
    public class DataList<T> : DatabaseEntry where T : BaseData
    {
        public List<T> entries = new List<T>();

        public int Count
        {
            get
            {
                return entries.Count;
            }
        }
    }
}