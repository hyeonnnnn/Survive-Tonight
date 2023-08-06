using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    // 프리팹 변수
    [SerializeField] GameObject goPrefab = null;

    // 몬스터 위치를 담을 리스트
    List<Transform> objectList = new List<Transform>();

    // Hp바 리스트
    List<GameObject> hpBarList = new List<GameObject>();

    Camera cam = null;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        AddHpBar();
    }

    // Update is called once per frame
    void Update()
    {
        ChaseMonster();
    }

    void AddHpBar()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < objects.Length; i++)
        {
            Enemy enemy = objects[i].GetComponent<Enemy>();

            if (enemy != null && (enemy.enemyName != ("Middle Boss") || enemy.enemyName != ("Final Boss")))
            {
                objectList.Add(objects[i].transform);
                GameObject hpBar = Instantiate(goPrefab, objects[i].transform.position, Quaternion.identity, transform);
                hpBarList.Add(hpBar);
            }
        }
    }

    void ChaseMonster()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i] != null)
            {
                Vector3 hpBarPos = objectList[i].position + new Vector3(0, 3.5f, 0);
                hpBarList[i].transform.position = cam.WorldToScreenPoint(hpBarPos);

                if (hpBarList[i].transform.position.z < 0)
                {
                    hpBarPos.y -= 2.4f;
                    hpBarList[i].transform.position = cam.WorldToScreenPoint(hpBarPos);
                }
            }

            else
            {
                Destroy(hpBarList[i]);
                hpBarList.RemoveAt(i);
                objectList.RemoveAt(i);
                i--;
            }
        }
    }

    public void RemoveHpBar(Transform monsterTransform)
    {
        int index = objectList.IndexOf(monsterTransform);
        if (index != -1)
        {
            Destroy(hpBarList[index]);
            hpBarList.RemoveAt(index);
            objectList.RemoveAt(index);
        }
    }
}
