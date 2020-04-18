using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collider_Behavior : MonoBehaviour
{
    public float damage;
    void Start()
    {

    }

    void FixedUpdate() {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag =="Hurtbox")
        {
            if (collision.gameObject.name == "Head")
            {
                Debug.Log("Head-Shot!");
                damage = 20;
                Debug.Log(damage);
            }
            else if (collision.gameObject.name == "Torso")
            {
                Debug.Log("Body-Shot!");
                damage = 15;
                Debug.Log(damage);
            }
        }
    }
}