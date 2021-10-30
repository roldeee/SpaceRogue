using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private float nextShootTime;
    [SerializeField] public float fireRate = 0.1f;
    [SerializeField] public Projectile projectile;

    // Update is called once per frame
    void Update()
    {
        if (CanShoot() && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        nextShootTime = Time.time + fireRate;
        Projectile newProjectile = Instantiate(projectile, transform.position, transform.parent.rotation);
        newProjectile.Fire(transform.parent.forward);
    }

    bool CanShoot() => Time.time >= nextShootTime;
}
