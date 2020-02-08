[System.Serializable]
public class EffectArgs
{
    public EffectType type;

    public ActorStatus state;

    //DamageType
    public ChangeAttributeEffectArgs recover;
    public ChangeAttributeEffectArgs damage;
}

[System.Serializable]
public struct ChangeAttributeEffectArgs
{
    public ActorAttributeType attribute;
    public float amount;
    public float variance;
    public bool amountAsPerc;
    public bool varianceAsPerc;
    public bool canCritical;
    public DamageType type;
}

public enum DamageType { Physical, Magical};