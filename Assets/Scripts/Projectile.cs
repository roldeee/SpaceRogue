using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage = 10;
   
    public void Fire(Vector3 direction)
    {
        direction.Normalize();
        transform.up = direction;
        GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.name.Contains("GunProjectile"))
        {
            Destroy(gameObject); // Destroy projectile on collisions.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Projectile"))
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Destroy(gameObject, 5f); // Destroy projectile after 5 seconds
    }
}
