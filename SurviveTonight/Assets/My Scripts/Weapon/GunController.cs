using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Gun currentGun;
    [SerializeField] public WeaponManager theWeaponManager;
    private float currentFireRate;

    private Vector3 originPos;

    private RaycastHit hitInfo;

    [SerializeField] private Camera theCam;

    void Start()
    {
        originPos = Vector3.zero;
    }

    void Update()
    {
        GunFireRateCalc();
        TryFire();
    }

    // 연사 속도 계산
    void GunFireRateCalc()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime;
    }

    // 발사 시도
    void TryFire()
    {
        if(Input.GetButton("Fire1") && currentFireRate <= 0)
        {
            Fire();
        }
    }

    // 발사 전 계산
    void Fire()
    {
        currentFireRate = currentGun.fireRate;
        Shoot();
    }

    // 발사 후 계산
    void Shoot()
    {
        currentGun.muzzleFlash.Play();
        Hit();
    }

    void Hit()
    {
        if (Physics.Raycast(theCam.transform.position, theCam.transform.forward, out hitInfo, 100)) 
        {
            if (hitInfo.transform.tag == "Enemy")
            {
                hitInfo.transform.GetComponent<EnemyController>().Damage(2, transform.position);
            }
        }
    }
}
