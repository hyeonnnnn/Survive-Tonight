using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeArea : MonoBehaviour
{
    public int damage;

    virtual public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("¡¢√À");

        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.Damage(damage, transform.position);
        }
    }
}
