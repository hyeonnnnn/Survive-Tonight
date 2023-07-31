using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : CloseWeaponController
{
    // 활성화 여부
    public static bool isActivate = true;

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
                if (hitInfo.transform.tag == "Enemy")
                {
                    Debug.Log("망치로 적 타격");
                    hitInfo.transform.GetComponent<Enemy>().Damage(1, transform.position);
                }

                isSwing = false;
            }
            yield return null;
        }
    }
}
