using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunAndNight : MonoBehaviour
{
    [SerializeField] float secondPerRealTimeSecond; // 게임 세계의 n초 = 현실 세계의 1초 
    
    //bool isNight = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime); // 태양 회전

        //if (transform.eulerAngles.x >= 170)
            //isNight = true;
        //else if (transform.eulerAngles.x <= 10)
            //isNight = false;

    }
}
