using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory System/Items/Equipment")]

public class Equipment : ItemObject
{
    public void Awake()
    {
        // Weapon is just default ItemType, you can change this on inspector to anytype you want!
        // Remember! if you create new equipment from this script, you must change the type to equipment only because equipment variable not same as food.
        type = ItemType.Weapon;
    }
}