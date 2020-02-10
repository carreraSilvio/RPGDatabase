﻿using UnityEngine;

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

}

[System.Serializable]
public class SkillUnlockArgs
{
    public int level;
    public int skillId;
}