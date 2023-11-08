using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory System/Items/Food")]
public class Food : ItemObject
{
    public int restoreHpValue;
    public void Awake()
    {
        itemType = ItemType.Food;
    }
}
