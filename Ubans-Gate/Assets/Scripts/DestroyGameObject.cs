using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    [SerializeField] private float objectDuration = 2.0f;

    private void Awake()
    {
        // {objectDuration} is counted immediately after {this.gameObject} appears[Awake].
        Destroy(gameObject, objectDuration);
    }
}