using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Inventory and Items variable
    [SerializeField] private InventoryObject inventory;
    [SerializeField] private InventoryObject equipment;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            equipment.Save();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
            equipment.Load();
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        equipment.Container.Clear();
    }
}