namespace BrightLib.RPGDatabase.Runtime
{
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
}