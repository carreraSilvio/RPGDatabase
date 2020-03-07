namespace BrightLib.RPGDatabase.Runtime
{
    [System.Serializable]
    public class ActorData : BaseData
    {
        public int classId;
        public int initialLevel = 1;

        public int initialWeapon = 1;

        public ActorData(int id) : base(id)
        {
        }
    }
}