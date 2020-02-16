using System.Collections.Generic;
using UnityEngine;

public class DataList <T> : DatabaseEntry where T : BaseData
{
    [SerializeField, HideInInspector]
    private int _entryUniqueId = 100;
    public List<T> entries = new List<T>();

    public int FetchUniqueId()
    {
        return _entryUniqueId++;
    }
}