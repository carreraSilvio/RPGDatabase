using System.Linq;
using UnityEngine;

namespace BrightLib.RPGDatabase.Runtime
{
    public class RPGDatabaseManager : DatabaseManager
    {
        public ActorClassData FetchClassData(int classDataId)
        {
            var classesList = FetchEntry<ActorClassDataList>();
            return classesList.entries.First(l => l.Id == classDataId);
        }


        /// <summary>
        /// Fetch the amount a given attribute will have in a given level
        /// </summary>
        /// <param name="actorId">The actor ID</param>
        /// <param name="actorLevel">What level the character is</param>
        /// <param name="attr">The attribute</param>
        /// <returns></returns>
        public int FetchAmount(int actorId, int actorLevel, ActorAttributeType attr)
        {
            int amount = 0;

            var actorData = FetchEntry<ActorDataList>().entries.First(l => l.Id == actorId);
            var classData = FetchEntry<ActorClassDataList>().entries.First(l => l.Id == actorData.classId);

            amount = FetchCurveValue(classData.FetchCurve(attr), actorLevel, attr);

            return amount;
        }

        private int FetchCurveValue(AnimationCurve curve, int actorLevel, ActorAttributeType attr)
        {
            var level = FetchAttributeData(ActorAttributeType.Level);

            float normalizedValue = Mathf.InverseLerp(level.start, level.end, actorLevel);
            float targetCurveValue = curve.Evaluate(normalizedValue);

            var attrData = FetchAttributeData(attr);
            return attrData.FetchAtCurvePoint(targetCurveValue);
        }

        private AttributeSpecData FetchAttributeData(ActorAttributeType type)
        {
            var list = FetchEntry<AttributeSpecDataList>();

            if (type == ActorAttributeType.Level)
                return list.entries.First(x => x.name == "Level");
            else if (type == ActorAttributeType.XP)
                return list.entries.First(x => x.name == "XP");
            else if (type == ActorAttributeType.HP)
                return list.entries.First(x => x.name == "HP");
            else if (type == ActorAttributeType.MP)
                return list.entries.First(x => x.name == "MP");

            //Else
            return list.entries.First(x => x.name == "Common");
        }
    }
}

