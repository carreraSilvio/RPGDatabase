using UnityEngine;

namespace BrightLib.RPGDatabase.Runtime
{
    [System.Serializable]
    public class AttributeSpecData : BaseData
    {
        public int start;
        public int end;

        public AttributeSpecData(int id) : base(id)
        {
        }

        public int FetchAtCurvePoint(float curveValue)
        {
            return Mathf.RoundToInt(Mathf.Lerp(start, end, curveValue));
        }
    }
}