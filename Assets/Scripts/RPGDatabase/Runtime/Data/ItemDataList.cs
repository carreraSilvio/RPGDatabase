using UnityEngine;

[CreateAssetMenu(fileName = nameof(ItemDataList), menuName = "Database/" + nameof(ItemDataList), order = 1)]
public class ItemDataList : DataList<ItemData>
{
}
