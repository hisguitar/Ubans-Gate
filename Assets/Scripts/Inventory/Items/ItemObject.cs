using UnityEngine;

public enum ItemType
{
    Default,
    Weapon,
    Armor,
    Helmet,
    Amulet, 
    Shoes,
    Food
}

public enum Attributes
{
    Strength,
    Defense, // Decrease "Damage in TakeDamage"
    Agility, // Increase "movement speed"
    Vitality, // Increase "MaxHp"
    Intelligence, // Increase "Heal"
    Charisma,
    Luck
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]

// Base class to create all items
public class ItemObject : ScriptableObject
{
    public Sprite uiDisplay;
    public bool stackable;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    public Item data = new Item();

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id = -1;
    public ItemBuff[] buffs;
    public Item()
    {
        Name = "";
        Id = -1;
    }
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.data.Id;
        buffs = new ItemBuff[item.data.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max)
            {
                attribute = item.data.buffs[i].attribute
            };
        }
    }
}

[System.Serializable]
public class ItemBuff : IModifiers
{
    public Attributes attribute;
    public int value;
    public int min; // buff min value roll
    public int max; // buff max value roll
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    void IModifiers.AddValue(ref int baseValue)
    {
        baseValue += value;
    }

    public void GenerateValue()
    {
        value = Random.Range(min, max);
    }
}