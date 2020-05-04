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

    Dictionary<string, int> attackValueTable = new Dictionary<string, int>();

    [FMODUnity.EventRef]
    FMOD.Studio.EventInstance AttackInstance;

    [FMODUnity.EventRef]
    public string AttackEvent = "";

    FMOD.Studio.PARAMETER_ID AttackParameterId;
    

    void Start()
    {
        playerManager = gameObject.GetComponent<Player_Manager>();
        AttackInstance = FMODUnity.RuntimeManager.CreateInstance(AttackEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(AttackInstance, GetComponent<Transform>(), GetComponent<Rigidbody>());
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

        FMOD.Studio.EventDescription AttackEventDescription;
        AttackInstance.getDescription(out AttackEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION AttackParameterDescription;
        AttackEventDescription.getParameterDescriptionByName("AttackOrder", out AttackParameterDescription);
        AttackParameterId = AttackParameterDescription.id;

        AttackInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            launchAttack("LightPunch");
            StartCoroutine(StartAttack(1.40f, hitboxes[0]));
            tester = attackBoxes[0];
            lastAttack = "LightPunch";
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            launchAttack("HeavyPunch");
            StartCoroutine(StartAttack(0.70f, hitboxes[0]));
            tester = attackBoxes[0];
            lastAttack = "HeavyPunch";
            
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {   //movementRequest.sendMovementRequest("o");
            launchAttack("LightKick");
            StartCoroutine(StartAttack(0.70f, hitboxes[1]));
            tester = attackBoxes[1];
            lastAttack = "LightKick";
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            launchAttack("HeavyKick");
            StartCoroutine(StartAttack(1.05f, hitboxes[1]));
            tester = attackBoxes[1];
            lastAttack = "HeavyKick";
        }
        AttackInstance.setParameterByID(AttackParameterId, attackNumber);
        if (_state == true)
        {
            Collider[] cols = Physics.OverlapBox(tester.bounds.center, tester.bounds.extents, tester.transform.rotation, LayerMask.GetMask("Hurtbox"));
            if (cols.Length>0)
            {
                int damage = getAttackValue(lastAttack);
                playerManager.GiveDamage(damage);
                Debug.Log("Success!");
                return;
            }
        }
    }
    // coroutine to activate the hitboxes only for the duration of the attack.
    IEnumerator StartAttack(float attacktime, GameObject hit)
    {
        hit.SetActive(true);
        _state = true;
        yield return new WaitForSeconds(attacktime);
        hit.SetActive(false);
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
            int damage = getAttackValue(attackName);
            playerManager.GiveDamage(damage); 
            
        }

    }

    /*void attackSound(string attackName)
    {
        if (!IsPlaying(attack)){
            FMODUnity.RuntimeManager.PlayOneShot(AttackEvent, transform.position);
            attack.start();
            attack.release();
        }
    }

    bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }*/
}