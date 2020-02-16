using UnityEngine;

public class BaseData 
{
    [SerializeField]
    private int _id;

    public string name;
    public int Id { get { return _id; } private set { _id = value; } }

    public void SetId(int id)
    {
        Id = id;
    }
}
