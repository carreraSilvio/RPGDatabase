namespace BrightLib.RPGDatabase.Runtime
{
    [System.Serializable]
    public class SkillData : BaseData
    {
        public string description;
        public float damage;

        public Usage usage;
        public Scope scope;

        public float hpCost;
        public float mpCost;

        public EffectArgs effect;

        public SkillData(int id) : base(id)
        {
            effect = new EffectArgs();
        }
    }
}