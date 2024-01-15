using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float fireRate = 0.15f;
    [SerializeField] private float bulletSpeed = 50.0f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;

    private float shotTime;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Perform();
        }
    }

    /* Ensures that the Fire() function can only be called once every {fireRate}
     * to control the rate of shooting or firing. */
    public void Perform()
    {
        if(Time.time - shotTime >= fireRate)
        {
            Fire();
            shotTime = Time.time;
        }
    }

    private void Fire()
    {
        // Create {bullet} in the location of {shootingPoint.position}
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.gameObject.transform.rotation);

        // Get the Rigidbody component attached to the {bullet} GameObject
        Rigidbody bullet_rb = bullet.GetComponent<Rigidbody>();

        // Calculate the direction and speed.
        bullet_rb.velocity = (bullet.transform.forward * bulletSpeed);
    }
}