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
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            launchAttack("HeavyPunch");
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            launchAttack("LightKick");
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            launchAttack("HeavyKick");
        }
        AttackInstance.setParameterByID(AttackParameterId, attackNumber);
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
            attackNumber = 1;
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