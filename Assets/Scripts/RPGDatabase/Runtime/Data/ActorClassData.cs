using UnityEngine;

[System.Serializable]
public class ActorClassData : BaseData
{
    public AnimationCurve expCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public AnimationCurve hpCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public AnimationCurve mpCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public AnimationCurve strCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public AnimationCurve magCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public AnimationCurve dexCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public AnimationCurve agiCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public AnimationCurve lckCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public AnimationCurve defCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public AnimationCurve resCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public int weaponTypeId;
    public SkillUnlockArgs[] skills;

    public AnimationCurve FetchCurve(ActorAttributeType type)
    {
        if (type == ActorAttributeType.HP)              return hpCurve;
        else if (type == ActorAttributeType.MP)         return mpCurve;

        else if (type == ActorAttributeType.Strength)   return strCurve;
        else if (type == ActorAttributeType.Magic)      return magCurve;

        else if (type == ActorAttributeType.Dextery)    return dexCurve;
        else if (type == ActorAttributeType.Agility)    return agiCurve;
        else if (type == ActorAttributeType.Luck)       return lckCurve;

        else if (type == ActorAttributeType.Defense)    return defCurve;
        else if (type == ActorAttributeType.Resistance) return resCurve;

        return expCurve;
    }

}

[System.Serializable]
public class SkillUnlockArgs
{
    public int level;
    public int skillId;
}