using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject[] bulletPrefab;
    public Transform[] shotOrigin;

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
                Instantiate(bulletPrefab[0], shotOrigin[0].position, shotOrigin[0].rotation);
                shootFactor = 0;
            }
        }
    }

    public void LastResort()
    {
        if (canShoot)
        {
            if (shootFactor >= shootRate)
            {
                Instantiate(bulletPrefab[1], shotOrigin[0].position, shotOrigin[0].rotation);
                Instantiate(bulletPrefab[1], shotOrigin[1].position, shotOrigin[1].rotation);
                shootFactor = 0;
            }
        }
    }
}
