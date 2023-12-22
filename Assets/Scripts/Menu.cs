using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private UIManager UIManager;
    [SerializeField] private GameObject adviceAlert;
    [SerializeField] private TMP_InputField nameInput;

    private void Start()
    {
        Time.timeScale = 0;
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
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }
}