using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;
    public float fireRate; // 연사 속도
    public int damage; // 데미지
    public ParticleSystem muzzleFlash;
}
