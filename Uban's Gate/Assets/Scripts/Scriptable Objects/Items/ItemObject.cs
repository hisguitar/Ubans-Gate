using UnityEngine;

public enum ItemType
{
    Default,
    Weapon,
    Equipment,
    Food
}

// Base class to create all items
public abstract class ItemObject : ScriptableObject
{
    public GameObject itemPrefab;
    public ItemType itemType;
    [TextArea(15, 20)]
    public string description;
}