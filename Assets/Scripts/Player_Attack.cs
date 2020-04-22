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
    public Player_Manager playerManager;

    Dictionary<string, int> attackValueTable = new Dictionary<string, int>();

    [FMODUnity.EventRef]
    public string PlayerStateEvent = "";
    FMOD.Studio.EventInstance playerState;

    [FMODUnity.EventRef]
    public string PreAttackEvent = "";
    [FMODUnity.EventRef]
    public string AttackEvent = "";

    void Start()
    {
        playerManager = gameObject.GetComponent<Player_Manager>();

        playerState = FMODUnity.RuntimeManager.CreateInstance(PlayerStateEvent);
        playerState.start();
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
        anim.SetTrigger(attackName);
        attackSound(attackName);
        int damage = getAttackValue(attackName);
        playerManager.GiveDamage(damage);
    }

    void attackSound(string attackName)
    {
        FMODUnity.RuntimeManager.PlayOneShot(PreAttackEvent, transform.position);
    }
}