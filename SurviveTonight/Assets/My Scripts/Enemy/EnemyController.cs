using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected GameObject go_Enemy;
    [SerializeField] public string enemyName;

    [SerializeField] protected int maxHp;
    protected int currentHp;

    [SerializeField] protected GameObject bonePrefab;
    [SerializeField] protected Item theItem; // ��� ������

    // ���º���
    protected bool isDead = false; // �׾�����
    protected  bool isAttack; // ���� ������
    protected bool isChase; // �Ѵ� ������

    // �ʿ��� ������Ʈ
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected BoxCollider boxCol;
    [SerializeField] protected BoxCollider meleeArea;
    [SerializeField] protected Transform target;
    [SerializeField] protected NavMeshAgent nav;

    // Ȱ��ȭ �� ���� �ʱ�ȭ (������Ʈ Ǯ��)
    private void OnEnable()
    {
        isDead = false;
        nav.enabled = true;
        isChase = true;
        isAttack = false;
        rigid.isKinematic = true;

        anim.SetBool("doDie", false);

        currentHp = maxHp;

        if (enemyName != "Final Boss")
            Invoke("ChaseStart", 2);
    }

    protected void ChaseStart()
    {
        isChase = true;

        if (enemyName != "Final Boss")
            anim.SetBool("isWalk", true);
    }

    void Update()
    {
        if (nav.enabled && (enemyName != "Final Boss"))
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

    // �÷��̾� ���󰡱�
    protected void Targeting()
    {
        if (!isDead && (enemyName != "Final Boss"))
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

    // �����ϱ�
    protected IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = true; // ���ݹ��� Ȱ��ȭ

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

    // ������ �ޱ�
    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if (isDead == false)
        {
            currentHp -= _dmg;

            if (currentHp <= 0)
            {
                Dead();
                return;
            }
        }
    }

    // �ױ�
    public virtual void Dead()
    {
        isDead = true;
        nav.enabled = false;
        isChase = false;
        rigid.isKinematic = false;
        anim.SetBool("doDie", true);
        DropItem();

        gameObject.SetActive(false);
        Spawner.instance.InsertQueue(gameObject);
    }

    // ������ ���
    public void DropItem()
    {
        for (int i = 0; i < Mathf.Round(Random.Range(1, 3)); i++)
        {
            Vector3 boneSpawnPosition = transform.position + new Vector3(0f, 2.5f, 0f);
            Instantiate(bonePrefab, boneSpawnPosition, Quaternion.identity);
        }
    }
}
