using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunAndNight : MonoBehaviour
{
    [SerializeField] float secondPerRealTimeSecond; // ���� ������ n�� = ���� ������ 1�� 
    
    //bool isNight = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime); // �¾� ȸ��

        //if (transform.eulerAngles.x >= 170)
            //isNight = true;
        //else if (transform.eulerAngles.x <= 10)
            //isNight = false;

    }
}
