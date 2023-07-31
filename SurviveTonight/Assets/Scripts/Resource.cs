using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public enum Type { InitHammer, StoneSculpture, Bone, Bullet, Torax, Key };
    public Type type;

    [HideInInspector] public int value = 1;
    [HideInInspector] public int initHammer, stoneSculpture, bone, bullet, torax, key = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 20 * Time.deltaTime);
    }
}
