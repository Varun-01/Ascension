using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public enum GameState { INTRO, MAIN_MENU }
    Vector3[] defaultPos1;
    Vector3[] defaultScale1;
    Quaternion[] defaultRot1;
    Transform[] models1;
    Vector3[] defaultPos2;
    Vector3[] defaultScale2;
    Quaternion[] defaultRot2;
    Transform[] models2;
    string p1 = "Player1";
    string p2 = "Player2";
     public int  ps1;
     public int  ps2;
    bool reset;
    

    //Attach Button from the Editor
   
    public float x = 99.0f;

    private static Game_Manager playerInstance; 
    void Awake() {
        DontDestroyOnLoad(this);

        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        reset = true;
        ps1 = 0;
        ps2 = 0;
        GameObject player1 = GameObject.FindWithTag(p1);
        GameObject player2 = GameObject.FindWithTag(p2);
        Player_Manager player1Manager = player1.GetComponent<Player_Manager>();
        Player_Manager player2Manager = player2.GetComponent<Player_Manager>();
        Invoke("ResetScene", x);

        
    }
    
    void ResetScene()
    {
        
        GameObject player1 = GameObject.FindWithTag(p1);
        GameObject player2 = GameObject.FindWithTag(p2);
        Player_Manager player1Manager = player1.GetComponent<Player_Manager>();
        Player_Manager player2Manager = player2.GetComponent<Player_Manager>();
        if (player1Manager.playerHealth > player2Manager.playerHealth)
        {
            ps1++;
            
        }
        if (player2Manager.playerHealth > player1Manager.playerHealth)
        {
            ps2++;
            
        }
        Application.LoadLevel(Application.loadedLevel);
        if (ps1 < 2 && ps2 < 2)
        {
                       
                Invoke("ResetScene", x);
                  
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        GameObject player1 = GameObject.FindWithTag(p1);
        GameObject player2 = GameObject.FindWithTag(p2);
        Player_Manager player1Manager = player1.GetComponent<Player_Manager>();
        Player_Manager player2Manager = player2.GetComponent<Player_Manager>();
        if (ps1 < 2 && ps2 < 2)
        {
            
            if (player1Manager.alive == 0)
            {
                ps2++;
                Application.LoadLevel(Application.loadedLevel);
                
                              
            }
            if (player2Manager.alive == 0)
            {
                ps1++;
                Application.LoadLevel(Application.loadedLevel);
                
                
            }
        }

    }
  
    
    
}











