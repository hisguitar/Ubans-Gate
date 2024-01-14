using System.Collections;
using UnityEngine;
using TMPro;

public class DropdownPopup : MonoBehaviour
{
    [SerializeField] private GameObject dropdownPopup;
    [SerializeField] private float slideTime = 0.5f;
    [SerializeField] private float displayTime = 2f;

    private void Start()
    {
        // Hide pop-up at start
        dropdownPopup.SetActive(false);
    }

    // Use only ShowPopup method.
    public void ShowPopup(string message)
    {
        dropdownPopup.SetActive(true);
        StartCoroutine(ShowAndHidePopup(message));
    }

    private IEnumerator ShowAndHidePopup(string message)
    {
        // Set pop-up text
        dropdownPopup.GetComponentInChildren<TMP_Text>().text = message;

        // Get current anchored position
        Vector2 startPos = new(0f, 60f); // Set y to 60f

        // Set target anchored position
        Vector2 endPos = new(startPos.x, -30f); // Set y to -40f

        // Slide in
        float elapsedTime = 0f;
        while (elapsedTime < slideTime)
        {
            dropdownPopup.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / slideTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Show pop-up (displayTime) sec.
        yield return new WaitForSeconds(displayTime);

        // Slide out
        elapsedTime = 0f;
        while (elapsedTime < slideTime)
        {
            dropdownPopup.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(endPos, startPos, elapsedTime / slideTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Close pop-up
        dropdownPopup.SetActive(false);
    }
}