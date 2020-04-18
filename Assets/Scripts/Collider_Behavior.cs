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
            Collider[] cols = Physics.OverlapBox(collision.bounds.center, collision.bounds.extents, collision.transform.rotation, LayerMask.GetMask("Hurtbox"), QueryTriggerInteraction.Collide);

            foreach (Collider c in cols)
                {
                    if (c.transform.root.name != transform.root.name)
                    {
                        Debug.Log(c.name);
                    }
                }
        }
    }
}