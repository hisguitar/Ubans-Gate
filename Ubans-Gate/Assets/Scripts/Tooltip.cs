using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Tooltip : MonoBehaviour
{
    public LayoutElement layoutElement;
    public TMP_Text headerText;
    public TMP_Text contentText;
    public int characterWrapLimit;
    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerText.gameObject.SetActive(false);
        }
        else
        {
            headerText.gameObject.SetActive(true);
            headerText.text = header;
        }
        contentText.text = content;

        int headerLength = headerText.text.Length;
        int contentLength = contentText.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;

        Vector2 position = Input.mousePosition;
        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;
        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }
}