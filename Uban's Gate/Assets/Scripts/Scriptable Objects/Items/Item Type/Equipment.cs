using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory System/Items/Equipment")]

public class Equipment : ItemObject
{
    public float attackBonus;
    public float defenceBonus;

    public void Awake()
    {
        itemType = ItemType.Equipment;
    }
}