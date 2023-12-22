using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("MENU")]
    [SerializeField] private UIManager UIManager;
    [SerializeField] private GameObject adviceAlert;
    [SerializeField] private TMP_InputField nameInput;

    [Header("THINGS TO HIDE")] // Player_UI, Console_UI
    [SerializeField] private GameObject[] hide;

    private void Start()
    {
        foreach (GameObject obj in hide)
        {
            obj.SetActive(false);
        }

        Time.timeScale = 0;

        // Input filter
        nameInput.contentType = TMP_InputField.ContentType.Alphanumeric;
    }

    public void NameInput()
    {
        if (nameInput.text.Length < 6)
        {
            adviceAlert.SetActive(true);
        }
        else
        {
            UIManager.username = nameInput.text;
            UIManager.alreadyHasName = true;

            foreach (GameObject obj in hide)
            {
                obj.SetActive(true);
            }

            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }
}