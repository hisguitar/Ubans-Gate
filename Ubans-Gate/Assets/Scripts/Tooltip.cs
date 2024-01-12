using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Tooltip : MonoBehaviour
{
    /// <summary>
    /// 1. Add this GameObject to "layoutElement" and "rectTransform"
    /// 2. Add component Vertical Layout Group, Set padding as you want and tick width and height of control child size
    /// 3. Add component Content Size Fitter, Set horizontal fit and vertical fit to "Preferred Size"
    /// 4. Add component Layout Element, Tick preferred width and set to about 500
    /// </summary>
    public LayoutElement layoutElement;
    public RectTransform rectTransform;
    public TMP_Text headerText;
    public TMP_Text contentText;
    public int characterWrapLimit;

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