using UnityEngine;

public class Toggle : MonoBehaviour
{
    public GameObject equipmentInventory;

    private void Update()
    {
        // Equipment and Inventory window
        if (Input.GetKeyUp(KeyCode.I))
        {
            if (equipmentInventory != null)
            {
                equipmentInventory.SetActive(!equipmentInventory.activeSelf);
            }
        }
    }

    public void ToggleWindow(GameObject window)
    {
        if (window != null)
        {
            window.SetActive(!window.activeSelf);
        }
    }
}