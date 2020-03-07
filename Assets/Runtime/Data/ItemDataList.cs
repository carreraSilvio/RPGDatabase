using UnityEngine;

namespace BrightLib.RPGDatabase.Runtime
{
    [CreateAssetMenu(fileName = nameof(ItemDataList), menuName = "Database/" + nameof(ItemDataList), order = 1)]
    public class ItemDataList : DataList<ItemData>
    {
    }
}