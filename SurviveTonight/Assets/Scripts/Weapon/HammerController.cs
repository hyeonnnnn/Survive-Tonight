using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : CloseWeaponController
{
    // Ȱ��ȭ ����
    public static bool isActivate = true;

    void Update()
    {
        if (isActivate)
            TryAttack();
    }

    protected override IEnumerator HitCoroutine() // ������
    {
        while (isSwing) // ���� üũ
        {
            if (checkObject())
            {
                if (hitInfo.transform.tag == "Enemy")
                {
                    Debug.Log("��ġ�� �� Ÿ��");
                    hitInfo.transform.GetComponent<Enemy>().Damage(1, transform.position);
                }

                isSwing = false;
            }
            yield return null;
        }
    }
}
