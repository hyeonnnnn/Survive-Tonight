using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject go_Enemy;
    [SerializeField] protected string enemyName;
    [SerializeField] protected int hp;
    [SerializeField] protected Transform target;
    [SerializeField] protected BoxCollider meleeArea;

    // 상태변수
    protected bool isDead = false; // 죽었는지
    protected  bool isAttack; // 공격 중인지
    protected bool isChase; // 쫓는 중인지

    // 필요한 컴포넌트
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected BoxCollider boxCol;
    [SerializeField] protected NavMeshAgent nav;

    void Awake()
    {
        if(enemyName != "Boss")
            Invoke("ChaseStart", 2);
    }

    protected void ChaseStart()
    {
        isChase = true;
        
        if(enemyName != "Boss")
            anim.SetBool("isWalk", true);
    }

    void Update()
    {
        if (nav.enabled && (enemyName != "Boss"))
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }
    }

    protected void FixedUpdate()
    {
        Targeting();
        FreezeVelocity();
    }

    protected void Targeting()
    {
        if (!isDead && enemyName != "Boss")
        {
            float targetRedius = 1.5f;
            float targetRange = 3f;

            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRedius, transform.forward, targetRange, LayerMask.GetMask("Player"));

            if (rayHits.Length > 0 && !isAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    protected IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = true; // 공격범위 활성화

        yield return new WaitForSeconds(1f);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(1f);

        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }

    protected void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if (isDead == false)
        {
            hp -= _dmg;

            if (hp<=0)
            {
                Dead();
                return;
            }
        }
    }

    protected void Dead()
    {
        isDead = true;
        nav.enabled = false;
        isChase = false;
        rigid.isKinematic = false;
        anim.SetBool("doDie", true);
        Destroy(go_Enemy, 1);
    }
}
