using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Spawner : MonoBehaviour
{
    public GameObject monster; // 스폰할 몬스터 프리팹
    public static Spawner instance; // 싱글톤 인스턴스

    public Queue<GameObject> mons_queue = new Queue<GameObject>(); // 몬스터 큐

    public float xPos;
    public float zPos;
    private Vector3 RandomVector;

    [SerializeField] Stage theStage;
    [SerializeField] DayAndNight dayAndNight;

    // Start is called before the first frame update
    void Start()
    {
        instance = this; // 인스턴스 설정

        // 몬스터 오브젝트 생성 후 오브젝트 풀에 추가
        for (int i = 0; i < 5; i++)
        {
            GameObject t_object = Instantiate(monster, SpawnPosition(), Quaternion.identity, this.gameObject.transform);
            
            mons_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
    }

    void Update()
    {
        StartCoroutine(MonsterSpawn());
    }

    // 무작위로 스폰 위치 생성
    Vector3 SpawnPosition()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        int randomIndex = Random.Range(0, navMeshData.vertices.Length);
        Vector3 randomPosition = navMeshData.vertices[randomIndex];
        return randomPosition;
    }

    // 오브젝트를 큐에 추가 (오브젝트 풀 관리)
    public void InsertQueue(GameObject p_object)
    {
        mons_queue.Enqueue(p_object);
        p_object.SetActive(false);
    }

    // 오브젝트 풀에서 오브젝트 꺼내기
    public GameObject GetQueue()
    {
        GameObject t_object = mons_queue.Dequeue();
        t_object.SetActive(true);

        return t_object;
    }

    // 밤에 몬스터 스폰
    IEnumerator MonsterSpawn()
    {
        while (true && dayAndNight.isNight)
        {
            if (mons_queue.Count != 0)
            {
                // 랜덤 위치 벡터 생성
                xPos = Random.Range(-5, 5);
                zPos = Random.Range(-5, 5);
                RandomVector = new Vector3(xPos, 0.0f, zPos);
                
                //큐에서 오브젝트를 가져와 위치 설정
                GameObject t_object = GetQueue();
                t_object.transform.position = gameObject.transform.position + RandomVector;
            }

            yield return new WaitForSeconds(21f);
        }
    }
}