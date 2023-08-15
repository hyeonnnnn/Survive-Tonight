using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapon : MonoBehaviour
{
    public string closeWeaponName; // 근접 무기 이름

    public bool isHammer; // 망치인지

    public int damage; // 공격력
    public float attackDelay; // 공격 딜레이
    public float attackDelayA; // 공격 활성화 시점 딜레이 (데미지O)
    public float attackDelayB; // 공격 비활성화 시점 딜레이 (데미지X)

    public Animator anim;
}
