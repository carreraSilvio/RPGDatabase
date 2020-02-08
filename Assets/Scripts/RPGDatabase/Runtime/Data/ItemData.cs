[System.Serializable]
public class ItemData : BaseData
{
    public string description;

    public ItemType type;
    public Usage usage;
    public Scope scope;

    public int price;

    public EffectArgs effect;

    public ItemData()
    {
        effect = new EffectArgs();
    }

}