using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory System/Items/Equipment")]

public class Equipment : ItemObject
{
    public void Awake()
    {
        type = ItemType.Equipment;
    }
}