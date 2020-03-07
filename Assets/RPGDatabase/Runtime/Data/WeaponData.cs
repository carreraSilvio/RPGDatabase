namespace BrightLib.RPGDatabase.Runtime
{
    [System.Serializable]
    public class WeaponData : BaseData
    {
        public string description;
        public int price;

        public int typeId;

        public int strIncrease;
        public int magIncrease;

        public int dexIncrease;
        public int agiIncrease;
        public int lckIncrease;

        public int defIncrease;
        public int resIncrease;

        public WeaponData(int id) : base(id)
        {
        }
    }
}