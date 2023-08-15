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
    // [SerializeField] protected TextMeshProUGUI hp_Text;
    // [SerializeField] protected GameObject hp_Holder;

    [SerializeField] protected GameObject bonePrefab;
    [SerializeField] protected Item theItem; // 드롭 아이템

    // 상태변수
    protected bool isDead = false; // 죽었는지
    protected  bool isAttack; // 공격 중인지
    protected bool isChase; // 쫓는 중인지

    // 필요한 컴포넌트
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected BoxCollider boxCol;
    [SerializeField] protected BoxCollider meleeArea;
    [SerializeField] protected Transform target;
    [SerializeField] protected NavMeshAgent nav;

    // 활성화 시 정보 초기화 (오브젝트 풀링)
    private void OnEnable()
    {
        isDead = false;
        nav.enabled = true;
        isChase = true;
        isAttack = false;
        rigid.isKinematic = true;

        anim.SetBool("doDie", false);

        // 멀어지면 아이콘도 작게하는 코드 추가하기
        /*
         void ChaseMonster()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i] != null)
            {
                Vector3 locationPos = objectList[i].position + new Vector3(0, 3.2f, 0);
                Vector3 screenPos = cam.WorldToScreenPoint(locationPos);

                if (screenPos.z < 0)
                {
                    screenPos *= -1f;
                    screenPos.z = 0;
                }

                locationList[i].transform.position = screenPos;

                float maxIconSize = 4.0f; // 아이콘의 최대 크기

                // 카메라와 아이콘의 Z축 거리 차이
                float zDistance = Mathf.Abs(cam.transform.position.z - objectList[i].position.z);

                // 거리 차이가 작아질수록 아이콘의 크기 커짐
                float iconSize = Mathf.Lerp(1.0f / maxIconSize, 2.0f, maxIconSize / zDistance);
                locationList[i].transform.localScale = new Vector3(iconSize, iconSize, 1f);
            }

            else
            {
                Destroy(locationList[i]);
                locationList.RemoveAt(i);
                objectList.RemoveAt(i);
                i--;
            }
        }
    }
         */
        currentHp = maxHp;

        // hp_Text.text = maxHp.ToString();
        // hp_Holder.SetActive(true);

        if (enemyName != "Middle Boss" || enemyName != "Final Boss")
            Invoke("ChaseStart", 2);
    }

    protected void ChaseStart()
    {
        isChase = true;

        if (enemyName != "Middle Boss" || enemyName != "Final Boss")
            anim.SetBool("isWalk", true);
    }

    void Update()
    {
        if (nav.enabled && (enemyName != "Middle Boss" || enemyName != "Final Boss"))
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }

        // Vector3 hp_text_Pos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 3.2f, 0));
        // hp_Holder.transform.position = hp_text_Pos;
    }

    protected void FixedUpdate()
    {
        Targeting();
        FreezeVelocity();
    }

    // 플레이어 따라가기
    protected void Targeting()
    {
        if (!isDead && (enemyName != "Middle Boss" || enemyName != "Final Boss"))
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

    // 공격하기
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

    // 데미지 받기
    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if (isDead == false)
        {
            currentHp -= _dmg;
            // hp_Text.text = currentHp.ToString();

            if (currentHp <= 0)
            {
                Dead();
                return;
            }
        }
    }

    // 죽기
    public virtual void Dead()
    {
        Debug.Log("죽음");
        isDead = true;
        nav.enabled = false;
        isChase = false;
        rigid.isKinematic = false;
        anim.SetBool("doDie", true);
        DropItem();

        gameObject.SetActive(false);
        // hp_Holder.SetActive(false);
        Spawner.instance.InsertQueue(gameObject);
    }

    // 아이템 드롭
    public void DropItem()
    {
        for (int i = 0; i < Mathf.Round(Random.Range(1, 3)); i++)
        {
            Vector3 boneSpawnPosition = transform.position + new Vector3(0f, 2.5f, 0f);
            Instantiate(bonePrefab, boneSpawnPosition, Quaternion.identity);
        }
    }
}
