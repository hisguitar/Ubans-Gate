using UnityEngine;

public class ToggleWindow : MonoBehaviour
{
    // Add this script to any window you need to open/close, Can be use to button.
    public void OnOpenOrClose()
    {
        if (gameObject.activeSelf == false)
        {
            LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.25f).setOnStart(Toggle);
        }
        else if (gameObject.activeSelf == true)
        {
            LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.25f).setOnComplete(Toggle);
        }
    }

    // Toggle window active/inactive
    private void Toggle()
    {
        if (gameObject != null)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}