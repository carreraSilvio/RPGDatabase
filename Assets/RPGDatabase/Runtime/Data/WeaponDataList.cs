using UnityEngine;

namespace BrightLib.RPGDatabase.Runtime
{
    [CreateAssetMenu(fileName = nameof(WeaponDataList), menuName = "Database/" + nameof(WeaponDataList), order = 1)]
    public class WeaponDataList : DataList<WeaponData>
    {

    }
}