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
        if (Input.GetKeyDown(KeyCode.U))
        {
            //LaunchAttack(attackboxes[0], "LightPunch");
            anim.SetTrigger("LightPunch");
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            //LaunchAttack(attackboxes[0], "HeavyPunch");
            anim.SetTrigger("HeavyPunch");
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            //LaunchAttack(attackboxes[1], "LightKick");
            anim.SetTrigger("LightKick");
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            //LaunchAttack(attackboxes[2], "HeavyKick");
            anim.SetTrigger("HeavyKick");
        }

    }

    /*
    private void LaunchAttack(Collider col, string attackname)
    {
        // Launch the animation for the attack
        anim.SetTrigger(attackname);
        // Determine what came in contact with the attack
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Hurtbox"), QueryTriggerInteraction.Collide);

        foreach (Collider c in cols)
            {
                if (c.transform.root.name != transform.root.name)
                {
                    Debug.Log(c.name);
                    Debug.Log(c.transform.root.name);
                }
            }
            // Determine the damage
        }*/
    }
