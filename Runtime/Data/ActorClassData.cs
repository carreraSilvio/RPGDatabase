using System.Collections.Generic;
using UnityEngine;

namespace BrightLib.RPGDatabase.Runtime
{
    [System.Serializable]
    public class ActorClassData : BaseData
    {
        public AnimationCurve expCurve = AnimationCurve.Linear(0, 0, 1, 1);

        public AnimationCurve hpCurve = AnimationCurve.Linear(0, 0, 1, 1);
        public AnimationCurve mpCurve = AnimationCurve.Linear(0, 0, 1, 1);

        public AnimationCurve strCurve = AnimationCurve.Linear(0, 0, 1, 1);
        public AnimationCurve intCurve = AnimationCurve.Linear(0, 0, 1, 1);

        public AnimationCurve dexCurve = AnimationCurve.Linear(0, 0, 1, 1);
        public AnimationCurve agiCurve = AnimationCurve.Linear(0, 0, 1, 1);

        public AnimationCurve lckCurve = AnimationCurve.Linear(0, 0, 1, 1);
        public AnimationCurve defCurve = AnimationCurve.Linear(0, 0, 1, 1);
        public AnimationCurve resCurve = AnimationCurve.Linear(0, 0, 1, 1);

        public int weaponTypeId;
        public List<SkillUnlockArgs> skills;

        public ActorClassData(int id) : base(id)
        {
            skills = new List<SkillUnlockArgs>();
        }

        public AnimationCurve FetchCurve(ActorAttributeType type)
        {
            if (type == ActorAttributeType.HP) return hpCurve;
            else if (type == ActorAttributeType.MP) return mpCurve;

            else if (type == ActorAttributeType.Strength) return strCurve;
            else if (type == ActorAttributeType.Intelligence) return intCurve;

            else if (type == ActorAttributeType.Dextery) return dexCurve;
            else if (type == ActorAttributeType.Agility) return agiCurve;
            else if (type == ActorAttributeType.Luck) return lckCurve;

            else if (type == ActorAttributeType.Defense) return defCurve;
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
}