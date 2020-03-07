using UnityEngine;

namespace BrightLib.RPGDatabase.Runtime
{
    public class BaseData
    {
        [SerializeField]
        protected int _id;

        public string name;

        public int Id { get { return _id; } private set { _id = value; } }

        public BaseData(int id)
        {
            _id = id;
        }
    }
}