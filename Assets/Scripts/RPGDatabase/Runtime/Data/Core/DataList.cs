using System.Collections.Generic;

public class DataList <T> : DatabaseEntry where T : BaseData
{
    private int _entryUniqueId = 100;
    public List<T> entries = new List<T>();

    public int FetchUniqueId()
    {
        return _entryUniqueId++;
    }
}