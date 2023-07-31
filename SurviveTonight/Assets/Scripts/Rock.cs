using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] int hp;

    [SerializeField] SphereCollider col;

    // 필요한 게임 오브젝트
    [SerializeField] GameObject go_rock;
    [SerializeField] GameObject go_debris;
    
    int destroyTime = 3;

    public void Mining()
    {
        hp--;
        if (hp <= 0)
            Destruction();
    }

    void Destruction()
    {
        col.enabled = false;
        Destroy(go_rock);
        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
