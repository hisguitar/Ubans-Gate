using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float destroyTime = 3.0f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}