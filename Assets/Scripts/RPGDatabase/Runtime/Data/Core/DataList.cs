using System.Collections.Generic;

public class DataList <T> : DatabaseEntry where T : BaseData
{
    public List<T> entries = new List<T>();
}