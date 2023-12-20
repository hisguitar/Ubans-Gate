using UnityEngine;

public class Toggle : MonoBehaviour
{
    public void ToggleWindow(GameObject window)
    {
        if (window != null)
        {
            window.SetActive(!window.activeSelf);
        }
    }
}