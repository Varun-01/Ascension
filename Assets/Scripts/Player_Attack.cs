using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
   
    private CapsuleCollider col;
    private Rigidbody rb;
    private float orgColHight;
    private Vector3 orgVectColCenter;
    private Animator anim;
    private AnimatorStateInfo currentBaseState;

    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        orgColHight = col.height;
        orgVectColCenter = col.center;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) {
            anim.SetTrigger("LightPunch");
        } else if (Input.GetKeyDown(KeyCode.I))
            {
                anim.SetTrigger("HeavyPunch");
            }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            anim.SetTrigger("LightKick");
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            anim.SetTrigger("HeavyKick");
        }

    }
}
