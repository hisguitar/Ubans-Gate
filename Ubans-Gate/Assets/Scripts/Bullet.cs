using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            // If it hits something else
            default:
                Destroy(gameObject);
                break;
        }
    }
}