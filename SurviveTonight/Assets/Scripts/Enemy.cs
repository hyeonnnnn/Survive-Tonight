using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject go_Enemy;
    [SerializeField] protected string enemyName;
    [SerializeField] protected int hp;

    Color originalColor;
    Material mat;

    // 상태변수
    protected bool isAttack; // 공격 중인지
    protected bool isWalking; // 걷는 중인지
    protected bool isDead = false; // 죽었는지

    // 필요한 컴포넌트
    // [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected BoxCollider boxCol;

    void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
        originalColor = mat.color;
    }

    public virtual void Damage(int _dmg, Vector3 _targetPos)
    {
        if (isDead == false)
        {
            hp -= _dmg;
            mat.color = Color.red;

            if (hp > 0)
                Invoke("RestoreColor", 0.1f);

            else if (hp <= 0)
            {
                Dead();
                return;
            }
        }
    }

    void Restore()
    {
        mat.color = originalColor;
    }

    protected void Dead()
    {
        isWalking = false;
        isDead = true;
        Debug.Log("적 사망");
        // anim.SetTrigger("Dead");
        Destroy(go_Enemy, 1);
    }
}
