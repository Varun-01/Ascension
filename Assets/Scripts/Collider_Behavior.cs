using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Behavior : MonoBehaviour
{
    void Start()
    {

    }

    void FixedUpdate() {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag !="ground")
        {
            Debug.Log(collision.gameObject.tag);
        }
    }
}