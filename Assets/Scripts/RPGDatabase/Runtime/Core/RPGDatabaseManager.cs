

using System.Linq;
using UnityEngine;

namespace RPGDatabase.Runtime.Core
{
    public class RPGDatabaseManager : DatabaseManager
    {
        public int FetchAmount(int actorId, int actorLevel, ActorAttributeType attr)
        {
            int amount = 0;

            var actorData = FetchEntry<ActorDataList>().entries.First<ActorData>(l => l.Id == actorId);
            var classData = FetchEntry<ActorClassDataList>().entries.First<ActorClassData>(l => l.Id == actorData.classId);

            amount = FetchCurveValue(classData.FetchCurve(attr), actorLevel, attr);
            
            return amount;
        }

        private int FetchCurveValue(AnimationCurve curve, int actorLevel, ActorAttributeType attr)
        {
            var list = FetchEntry<AttributeSpecDataList>();
            var level = FetchAttributeData(ActorAttributeType.Level);

            float normalizedValue = Mathf.InverseLerp(level.start, level.end, actorLevel);
            float targetCurveValue = curve.Evaluate(normalizedValue);

            var attrData = FetchAttributeData(attr);
            return attrData.FetchAtCurvePoint(targetCurveValue);
        }

        private AttributeSpecData FetchAttributeData(ActorAttributeType type)
        {
            var list = FetchEntry<AttributeSpecDataList>();

            if(type == ActorAttributeType.Level)
                return list.entries.First<AttributeSpecData>(x => x.name == "Level");
            else if (type == ActorAttributeType.XP)
                return list.entries.First<AttributeSpecData>(x => x.name == "XP");
            else if (type == ActorAttributeType.HP)
                return list.entries.First<AttributeSpecData>(x => x.name == "HP");
            else if (type == ActorAttributeType.MP)
                return list.entries.First<AttributeSpecData>(x => x.name == "MP");

            //Else
            return list.entries.First<AttributeSpecData>(x => x.name == "Common");
        }
    }
}

