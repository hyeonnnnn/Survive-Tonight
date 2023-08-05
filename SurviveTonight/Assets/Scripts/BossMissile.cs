using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMissile : MeleeArea
{
    public Transform target;
    [SerializeField] NavMeshAgent nav;

    void Update()
    {
        nav.SetDestination(target.position);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        StartCoroutine("DestroyAfterDelay");
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
