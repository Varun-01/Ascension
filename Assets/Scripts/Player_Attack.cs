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
    public bool hit = false;
    public float hit_damage;

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
            LaunchAttack(attackboxes[0], "LightPunch");
        } 
        else if (Input.GetKeyDown(KeyCode.I))
        {
            LaunchAttack(attackboxes[0], "HeavyPunch");
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            LaunchAttack(attackboxes[1], "LightKick");
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            LaunchAttack(attackboxes[2], "HeavyKick");
        }

    }
    private void LaunchAttack(Collider co, string attackname)
    {
        // Launch the animation for the attack
        anim.SetTrigger(attackname);
        // Determine what came in contact with the attack
        Collider[] cols = Physics.OverlapBox(co.bounds.center, co.bounds.extents, co.transform.rotation, LayerMask.GetMask("Hurtbox"), QueryTriggerInteraction.Collide);
        foreach (Collider c in cols)
        {
            if (c.transform.root.name == transform.root.name)
            {
                continue;
            }
            else
            {
                Debug.Log(c.name);
                hit = true;
            }
            
        }
        // Determine the damage
        if (hit)
        {
            switch (attackname)
            {
                case "LightPunch":
                    hit_damage = 10;
                    break;
                case "HeavyPunch":
                    hit_damage = 20;
                    break;
                case "LightKick":
                    hit_damage = 15;
                    break;
                case "HeavyKick":
                    hit_damage = 30;
                    break;
                default:
                    Debug.Log("Something's Wrong");
                    break;
            }
        }
    }
}
