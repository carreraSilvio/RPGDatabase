
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

    public SkillData()
    {
        effect = new EffectArgs();
    }
}
