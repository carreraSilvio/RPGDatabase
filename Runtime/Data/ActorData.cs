using UnityEngine;

namespace BrightLib.RPGDatabase.Runtime
{
    [System.Serializable]
    public class ActorData : BaseData
    {
        public int classId;
        public int initialLevel = 1;

        public ActorGraphicsGroup graphics;

        public int initialWeapon = 1;

        public ActorData(int id) : base(id)
        {
        }

        [System.Serializable]
        public struct ActorGraphicsGroup
        {
            public Sprite face;
            public Sprite battler;
            public Sprite body;
        }
    }
}