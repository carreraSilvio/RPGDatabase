using UnityEngine;

namespace BrightLib.RPGDatabase.Runtime
{
    [CreateAssetMenu(fileName = nameof(AttributeSpecDataList), menuName = "Database/" + nameof(AttributeSpecDataList), order = 1)]
    public class AttributeSpecDataList : DataList<AttributeSpecData>
    {

    }
}