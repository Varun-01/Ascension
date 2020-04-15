using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public int playerHealth = 1000;
    public int winCount = 0;
    public float stunTime = .5f;
    public float knockDownTime = .5f;
    public string currentState = "idle";
    public float currentPosition;
    public string characterName;
    public string playerTag;
    public int defenseStat;
    public int attackStat;

    [FMODUnity.EventRef]
    public string PlayerStateEvent = "";

    // Start is called before the first frame update
    void Start()
    {
        playerTag = gameObject.tag;
        Debug.Log(playerTag);
        Player_Stats playerStats = gameObject.GetComponent<Player_Stats>();
        characterName = gameObject.name;
        Debug.Log(characterName);
        attackStat = playerStats.getPlayerAttack(characterName);
        Debug.Log(attackStat);
        defenseStat = playerStats.getPlayerDefense(characterName);
        Debug.Log(defenseStat);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    void TakeDamage()
    { 
    
    }

    void GiveDamage()
    {

    }

    void EndGame()
    { 
    
    }

    void AddWin() { 
    
    }
}
