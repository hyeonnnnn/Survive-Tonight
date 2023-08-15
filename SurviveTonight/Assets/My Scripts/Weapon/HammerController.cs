using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : CloseWeaponController
{
    // 활성화 여부
    public static bool isActivate = false;

    void Update()
    {
        if (isActivate)
            TryAttack();
    }

    protected override IEnumerator HitCoroutine() // 재정의
    {
        while (isSwing) // 공격 체크
        {
            if (checkObject())
            {
                // 바위
                if (hitInfo.transform.tag == "Rock")
                    hitInfo.transform.GetComponent<Rock>().Mining();

                // 몬스터
                if (hitInfo.transform.tag == "Enemy")
                {
                    Debug.Log("Hit");
                    hitInfo.transform.GetComponent<EnemyController>().Damage(1, transform.position);
                }

                isSwing = false;
            }
            yield return null;
        }
    }
}
