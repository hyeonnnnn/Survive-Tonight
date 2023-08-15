using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 미완성 클래스. 추상 클래스.
public abstract class CloseWeaponController : MonoBehaviour
{
    [SerializeField] protected CloseWeapon currentCloseWeapon; 

    protected bool isAttack = false;
    protected bool isSwing = false;

    protected RaycastHit hitInfo;
    [SerializeField] protected LayerMask layerMask;

    protected void TryAttack()
    {
        if (Input.GetButton("Fire1"))
            {
            if (!isAttack)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    protected IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentCloseWeapon.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentCloseWeapon.attackDelayA);

        // 휘두른 순간에만 적중
        isSwing = true;
        StartCoroutine(HitCoroutine());
        yield return new WaitForSeconds(currentCloseWeapon.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentCloseWeapon.attackDelay - currentCloseWeapon.attackDelayA - currentCloseWeapon.attackDelayB);
        isAttack = false; // 다시 공격
    }

    protected abstract IEnumerator HitCoroutine(); // 공격 적중을 알아보는 코루틴

    protected bool checkObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 10, layerMask))
        {
            return true;
        }
        return false;
    }
}