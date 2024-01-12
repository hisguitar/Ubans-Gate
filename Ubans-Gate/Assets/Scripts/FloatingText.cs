using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float destroyTime = 0.4f;
    [SerializeField] private float randomX = 0.5f;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(35f, 0f, 0f);
        Destroy(gameObject, destroyTime);

        /// random position x only
        /// -x to x 
        transform.localPosition += new Vector3(Random.Range(-randomX, randomX), 0.0f, 0.0f);
    }
}