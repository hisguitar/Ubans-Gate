using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    public ItemObject item;

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
    }
}