using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shotOrigin;

    public float shootRate = 1f;
    public float shootFactor = 0f;

    public void Shoot()
    {
        shootFactor += Time.deltaTime;
        if (shootFactor >= shootRate)
        {
            Instantiate(bulletPrefab, shotOrigin.position, shotOrigin.rotation);
            shootFactor = 0;
        }
    }
}
