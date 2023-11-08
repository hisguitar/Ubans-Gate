using UnityEngine;

// Every time that you create an object, The Item script will create default object from this class
[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Default")]
public class ItemDefault : ItemObject
{
    public void Awake()
    {
        itemType = ItemType.Default;
    }
}