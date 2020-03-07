namespace BrightLib.RPGDatabase.Runtime
{
    [System.Serializable]
    public class EffectArgs
    {
        public EffectType type;

        public ActorStatus state;

        //DamageType
        public ChangeAttributeEffectArgs recover;
        public ChangeAttributeEffectArgs damage;
    }
}