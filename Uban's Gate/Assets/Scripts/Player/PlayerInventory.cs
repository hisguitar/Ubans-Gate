using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    /// Inventory and Items variable
    [SerializeField] private InventoryObject playerInventory;

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            playerInventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerInventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            playerInventory.Load();
        }
    }

    private void OnApplicationQuit()
    {
        playerInventory.Container.Clear();
    }
}