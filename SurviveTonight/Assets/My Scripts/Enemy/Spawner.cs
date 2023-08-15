using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Spawner : MonoBehaviour
{
    public GameObject monster; // ������ ���� ������
    public static Spawner instance; // �̱��� �ν��Ͻ�

    public Queue<GameObject> mons_queue = new Queue<GameObject>(); // ���� ť

    public float xPos;
    public float zPos;
    private Vector3 RandomVector;

    [SerializeField] Stage theStage;
    [SerializeField] DayAndNight dayAndNight;

    // Start is called before the first frame update
    void Start()
    {
        instance = this; // �ν��Ͻ� ����

        // ���� ������Ʈ ���� �� ������Ʈ Ǯ�� �߰�
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

    // �������� ���� ��ġ ����
    Vector3 SpawnPosition()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        int randomIndex = Random.Range(0, navMeshData.vertices.Length);
        Vector3 randomPosition = navMeshData.vertices[randomIndex];
        return randomPosition;
    }

    // ������Ʈ�� ť�� �߰� (������Ʈ Ǯ ����)
    public void InsertQueue(GameObject p_object)
    {
        mons_queue.Enqueue(p_object);
        p_object.SetActive(false);
    }

    // ������Ʈ Ǯ���� ������Ʈ ������
    public GameObject GetQueue()
    {
        GameObject t_object = mons_queue.Dequeue();
        t_object.SetActive(true);

        return t_object;
    }

    // �㿡 ���� ����
    IEnumerator MonsterSpawn()
    {
        while (true && dayAndNight.isNight)
        {
            if (mons_queue.Count != 0)
            {
                // ���� ��ġ ���� ����
                xPos = Random.Range(-5, 5);
                zPos = Random.Range(-5, 5);
                RandomVector = new Vector3(xPos, 0.0f, zPos);
                
                //ť���� ������Ʈ�� ������ ��ġ ����
                GameObject t_object = GetQueue();
                t_object.transform.position = gameObject.transform.position + RandomVector;
            }

            yield return new WaitForSeconds(21f);
        }
    }
}