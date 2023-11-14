using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory System/Items/Food")]
public class Food : ItemObject
{
    public void Awake()
    {
        type = ItemType.Food;
    }
}
