using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Projectile projectile = collision.collider.GetComponent<Projectile>();
        if (projectile != null)
        {
            Destroy(gameObject);
        }
    }
}
