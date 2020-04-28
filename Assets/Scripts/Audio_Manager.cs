using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string PlayerStateEvent = "";
    FMOD.Studio.EventInstance playerState;

    [FMODUnity.EventRef]
    public string AttackEvent = "";
    FMOD.Studio.EventInstance attack;

    [FMODUnity.EventRef]
    public string StunEvent = "";
    FMOD.Studio.EventInstance stun;

    // Start is called before the first frame update
    void Start()
    {
        playerState = FMODUnity.RuntimeManager.CreateInstance(PlayerStateEvent);
        attack = FMODUnity.RuntimeManager.CreateInstance(AttackEvent);
        stun = FMODUnity.RuntimeManager.CreateInstance(AttackEvent);

    }

    // Update is called once per frame
    void Update()
    {
        attack.start();
        attack.release();
    }
}
