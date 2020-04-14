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
    public Collider[] attackboxes;

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
            LaunchAttack(attackboxes[0]);
        } 
        else if (Input.GetKeyDown(KeyCode.I))
        {
            anim.SetTrigger("HeavyPunch");
            LaunchAttack(attackboxes[0]);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            anim.SetTrigger("LightKick");
            LaunchAttack(attackboxes[1]);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            anim.SetTrigger("HeavyKick");
            LaunchAttack(attackboxes[2]);
        }
    }
    private void LaunchAttack(Collider col)
    {
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("HitBox"));
        foreach (Collider c in cols)
        {
            Debug.Log(c.name);           
        }
    }
}
