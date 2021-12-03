using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public GameObject Barrel, Explosion;
    [SerializeField] private float range;

    private bool exploded = false;
    private AudioSource audioSource;

    private void Awake()
    {
        Barrel.SetActive(true);
        Explosion.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void Explode()
    {
        exploded = true;
        if (Barrel != null)
        {
            Barrel.SetActive(false);
        }
        if (Explosion != null)
        {
            Explosion.SetActive(true);
        }
        audioSource.Play();

        Collider[] otherColliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider other in otherColliders)
        {
            if (other.GetComponent<EnemyAI>() != null)
            {
                other.GetComponent<EnemyAI>().TakeExplosiveDamage(100, transform.position);
            }
        }
        GetComponent<BoxCollider>().enabled = false;
        Destroy(gameObject, 5f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("GunProjectile"))
        {
            Explode();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Projectile") && !exploded)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Explode();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
