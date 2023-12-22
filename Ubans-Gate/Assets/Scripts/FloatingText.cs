using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float destroyTime = 0.5f;
    private Vector3 randomizeIntensity = new Vector3(0.5f, 0, 0);

    private void Start()
    {
        Destroy(gameObject, destroyTime);

        transform.localPosition += new Vector3(Random.Range(-randomizeIntensity.x, randomizeIntensity.x),
            Random.Range(-randomizeIntensity.y, randomizeIntensity.y),
            Random.Range(-randomizeIntensity.z, randomizeIntensity.z));
    }
}