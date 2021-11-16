using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public enum FIRE_TYPE { SINGLE_FIRE, DOUBLE_FIRE, SPREAD_FIRE };
    private float nextShootTime;
    private PlayerDataManager playerDataManager;
    [SerializeField] public float fireRate = 0.1f;
    [SerializeField] public Projectile projectile;
    [SerializeField] public Projectile doubleFireProjectile;
    [SerializeField] public Projectile spreadFireProjectile;
    [SerializeField] public float spreadFireRate = 0.12f;
    private FIRE_TYPE currentFireType;

    private void Start()
    {
        playerDataManager = PlayerDataManager.Instance;
        currentFireType = playerDataManager.playerData.currentFireType;
    }

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
        if (currentFireType == FIRE_TYPE.SINGLE_FIRE)
        {
            nextShootTime = Time.time + fireRate;
            Projectile newProjectile = Instantiate(projectile, transform.position + transform.forward, transform.rotation);
            newProjectile.Fire(transform.forward);
            EventManager.TriggerEvent<PlayerShootsEvent, Vector3>(transform.position);
        } else if (currentFireType == FIRE_TYPE.DOUBLE_FIRE)
        {
            nextShootTime = Time.time + fireRate;
            Projectile newProjectileL = Instantiate(doubleFireProjectile, transform.position + transform.forward + (-transform.right * 0.2f), transform.rotation);
            Projectile newProjectileR = Instantiate(doubleFireProjectile, transform.position + transform.forward + (transform.right * 0.2f), transform.rotation);
            newProjectileL.Fire(newProjectileL.transform.forward);
            newProjectileR.Fire(newProjectileR.transform.forward);
            EventManager.TriggerEvent<PlayerShootsEvent, Vector3>(transform.position);
        } else if (currentFireType == FIRE_TYPE.SPREAD_FIRE)
        {
            nextShootTime = Time.time + spreadFireRate;
            Quaternion left = Quaternion.Euler(0, -15, 0);
            Quaternion right = Quaternion.Euler(0, 15, 0);
            Projectile newProjectile = Instantiate(spreadFireProjectile, transform.position + transform.forward, transform.rotation);
            Projectile newProjectileL = Instantiate(spreadFireProjectile, transform.position + transform.forward, transform.rotation * left);
            Projectile newProjectileR = Instantiate(spreadFireProjectile, transform.position + transform.forward, transform.rotation * right);
            newProjectile.Fire(transform.forward);
            newProjectileL.Fire(newProjectileL.transform.forward);
            newProjectileR.Fire(newProjectileR.transform.forward);
            EventManager.TriggerEvent<PlayerShootsEvent, Vector3>(transform.position);
        }
    }

    public void SetFireType(FIRE_TYPE fireType)
    {
        currentFireType = fireType;
        playerDataManager.playerData.currentFireType = fireType;
    }

    bool CanShoot() => Time.time >= nextShootTime;
}
