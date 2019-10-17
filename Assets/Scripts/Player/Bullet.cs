using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10f;

    void Start()
    {
        rb.velocity = transform.forward * speed * Time.deltaTime;
    }
}
