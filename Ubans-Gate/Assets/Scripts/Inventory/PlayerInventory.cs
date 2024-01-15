using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Player player;

    // Inventory and Items variable
    [SerializeField] private InventoryObject inventory;
    [SerializeField] private InventoryObject equipment;

    // Add number of this attributes in the Inspector to = the number of Attributes of the "ItemObject" class.
    public Attribute[] attributes;

    public void Start()
    {
        player = GetComponent<Player>();

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }
    }

    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                // SetActive(false) GameObject named "BeginnerStaffPrefab" in Player class
                player.BeginnerStaffPrefab.SetActive(false);

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                    }
                }

                // Update playerData when removed item from equipment slot
                OnRemoveItemUpdate();

                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }

    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Placed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                // Check if _slot.ItemObject is "Beginner_Staff"
                if (_slot.ItemObject.name == "Beginner_Staff")
                {
                    // SetActive(true) GameObject named "BeginnerStaffPrefab" in Player class
                    player.BeginnerStaffPrefab.SetActive(true);
                }

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                    }
                }

                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var groundItem = other.GetComponent<GroundItem>();
        if (groundItem)
        {
            Item _item = new Item(groundItem.item);
            if (inventory.AddItem(_item, 1))
            {
                Destroy(other.gameObject);
            }
        }
    }

    public void SaveInventory()
    {
        inventory.Save();
        equipment.Save();
    }
    public void LoadInventory()
    {
        inventory.Load();
        equipment.Load();
    }
    
    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));

        // Call Updrade-stats method of playerData to update attributes value
        /* These functions cannot be combined into a single state because of the different values. must work separately */
        // Str
        if (attribute.type == Attributes.Strength)
        {
            player.UpdateStrength(attribute.value.ModifiedValue);
        }
        // Def
        if (attribute.type == Attributes.Defense)
        {
            player.UpdateDefense(attribute.value.ModifiedValue);
        }
        // Agi
        if (attribute.type == Attributes.Agility)
        {
            player.UpdateAgility(attribute.value.ModifiedValue);
        }
        // Vit
        if (attribute.type == Attributes.Vitality)
        {
            player.UpdateVitality(attribute.value.ModifiedValue);
        }
        // Int
        if (attribute.type == Attributes.Intelligence)
        {
            player.UpdateIntelligence(attribute.value.ModifiedValue);
        }
        // Lck
        if (attribute.type == Attributes.Luck)
        {
            player.UpdateLuck(attribute.value.ModifiedValue);
        }
    }

    // Update player stats to no greater than the maximum value.
    public void OnRemoveItemUpdate()
    {
        if (player.Hp > player.MaxHp)
        {
            player.Heal(0);
        }
        if (player.Mp > player.MaxMp)
        {
            player.IncreaseMp(0);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }
}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public PlayerInventory parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(PlayerInventory _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }
    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}