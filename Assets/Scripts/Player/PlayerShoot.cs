using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shotOrigin;

    public float shootRate = 1f;
    public float shootFactor = 0f;
    public bool canShoot = false;


    public void Update()
    {
        shootFactor += Time.deltaTime;
    }

    public void Shoot()
    {
        if (canShoot)
        {
            if (shootFactor >= shootRate)
            {
                Instantiate(bulletPrefab, shotOrigin.position, shotOrigin.rotation);
                shootFactor = 0;
            }
        }
    }
}
