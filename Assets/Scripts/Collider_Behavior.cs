using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collider_Behavior : MonoBehaviour
{
    public bool attacked = false;
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag =="Hurtbox")
        {
            attacked = true;
        }
    }
    public void Hello()
    {
        Debug.Log("Hello");
    }
}