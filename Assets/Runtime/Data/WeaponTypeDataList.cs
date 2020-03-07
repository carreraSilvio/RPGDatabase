using UnityEngine;

namespace BrightLib.RPGDatabase.Runtime
{
    [CreateAssetMenu(fileName = nameof(WeaponTypeDataList), menuName = "Database/" + nameof(WeaponTypeDataList), order = 1)]
    public class WeaponTypeDataList : DataList<WeaponTypeData>
    {

    }
}