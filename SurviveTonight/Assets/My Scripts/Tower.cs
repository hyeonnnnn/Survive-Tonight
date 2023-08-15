using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
    bool isShoot; // 발사 중인지 여부

    [SerializeField] public int dmg; // 타워의 공격력
    [SerializeField] public float shootDelay; // 발사 딜레이
    [SerializeField] protected SphereCollider atkRange; // 공격 범위
    [SerializeField] ParticleSystem shotFlash;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!isShoot)
            {
                StartCoroutine(Shoot(other.gameObject)); // 발사 코루틴 실행

            }
        }
    }

    void GetDamage(EnemyController enemyController)
    {
        enemyController.Damage(dmg, transform.position); // 공격 대상에게 데미지 입히기
        shotFlash.Play();
    }

    IEnumerator Shoot(GameObject target)
    {
        isShoot = true;

        EnemyController enemyController = target.GetComponent<EnemyController>();
        
        if (enemyController != null)
        {
            GetDamage(enemyController);
        }

        yield return new WaitForSeconds(shootDelay);
        isShoot = false;
    }
}



