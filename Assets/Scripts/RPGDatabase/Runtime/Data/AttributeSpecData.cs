using UnityEngine;

[System.Serializable]
public class AttributeSpecData : BaseData
{
    public int start;
    public int end;

    public int FetchAtCurvePoint(float curveValue)
    {
        return Mathf.RoundToInt(Mathf.Lerp(start, end, curveValue));
    }
}
