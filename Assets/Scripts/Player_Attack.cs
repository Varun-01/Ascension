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
    public string characterName;
    public string playerTag;
    public int attackNumber;
    public Player_Manager playerManager;
    
    public GameObject[] hitboxes;
    public Collider[] attackBoxes;
    private bool _state;
    private string lastAttack;
    private Collider tester;
    public Move moveRequest;
    private bool detected;
    private bool sent;
    private bool hit;


    Dictionary<string, int> attackValueTable = new Dictionary<string, int>();
    /*
    [FMODUnity.EventRef]
    FMOD.Studio.EventInstance AttackInstance;

    [FMODUnity.EventRef]
    public string AttackEvent = "";

    FMOD.Studio.PARAMETER_ID AttackParameterId;
    */

    void Start()
    {
        playerManager = gameObject.GetComponent<Player_Manager>();
        //AttackInstance = FMODUnity.RuntimeManager.CreateInstance(AttackEvent);
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(AttackInstance, GetComponent<Transform>(), GetComponent<Rigidbody>());
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        orgColHight = col.height;
        orgVectColCenter = col.center;
        //attack = FMODUnity.RuntimeManager.CreateInstance(AttackEvent);
        foreach (GameObject box in hitboxes)
        {
            box.SetActive(false);
        }

/*
        FMOD.Studio.EventDescription AttackEventDescription;
        AttackInstance.getDescription(out AttackEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION AttackParameterDescription;
        AttackEventDescription.getParameterDescriptionByName("AttackOrder", out AttackParameterDescription);
        AttackParameterId = AttackParameterDescription.id;
        AttackInstance.start();
        */
       sent = false;

        moveRequest = gameObject.GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.getControllable())
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                moveRequest.sendMoveRequest("U");
                launchAttack("LightPunch");
                StartCoroutine(StartAttack(1.40f, hitboxes[0]));
                tester = attackBoxes[0];
                lastAttack = "LightPunch";
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                moveRequest.sendMoveRequest("I");
                launchAttack("HeavyPunch");
                StartCoroutine(StartAttack(0.70f, hitboxes[0]));
                tester = attackBoxes[0];
                lastAttack = "HeavyPunch";

            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                moveRequest.sendMoveRequest("O");
                launchAttack("LightKick");
                StartCoroutine(StartAttack(0.70f, hitboxes[1]));
                tester = attackBoxes[1];
                lastAttack = "LightKick";
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                moveRequest.sendMoveRequest("P");
                launchAttack("HeavyKick");
                StartCoroutine(StartAttack(1.05f, hitboxes[1]));
                tester = attackBoxes[1];
                lastAttack = "HeavyKick";
            }
            //AttackInstance.setParameterByID(AttackParameterId, attackNumber);
            if (_state == true)
            {
                Collider[] cols = Physics.OverlapBox(tester.bounds.center, tester.bounds.extents, tester.transform.rotation, LayerMask.GetMask("Hurtbox"));
                if (cols.Length > 0)
                {
                    foreach (Collider c in cols)
                    {
                        if (c.transform.root.tag != tester.transform.root.tag)
                        {
                            detected = true;
                            if (detected == true && sent != true)
                            {
                                int damage = getAttackValue(lastAttack);
                                playerManager.GiveDamage(damage);
                                Debug.Log("Success");
                                sent = true;
                            }
                        }
                    }
                }
            }
            else
            {}
        }
    }
    // coroutine to activate the hitboxes only for the duration of the attack.
    IEnumerator StartAttack(float attacktime, GameObject hit)
    {
        hit.SetActive(true);
        _state = true;
        // keeps the attackbox active for the attack duration
        yield return new WaitForSeconds(attacktime);
        hit.SetActive(false);
        sent = false;
        _state = false;
    }


    void initAttackValueTable()
    {
        //attackValueTable.Add("animation", 0);
        attackValueTable.Add("LightPunch", 2);
        attackValueTable.Add("LightKick", 2);
        attackValueTable.Add("HeavyPunch", 5);
        attackValueTable.Add("HeavyKick", 5);
    }

    public int getAttackValue(string attackName)
    {
        if (!attackValueTable.ContainsKey(attackName))
        {
            return 0;
        }
        return attackValueTable[attackName];
    }

    public void launchAttack(string attackName)
    {

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Debug.Log("In attack mode, attack not counted");
            return;

        }
        else {
            anim.SetTrigger(attackName);
            //attackSound(attackName);
            //int damage = getAttackValue(attackName);
            //playerManager.GiveDamage(damage); 
            
        }

    }

    public void launchAttackFromNet(string attackName)
    {

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Debug.Log("In attack mode, attack not counted");
            return;

        }
        else {
            anim.SetTrigger(attackName);
            //attackSound(attackName);
            //int damage = getAttackValue(attackName);
            //playerManager.GiveDamage(damage); 
            
        }

    }


}