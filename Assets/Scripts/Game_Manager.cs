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
    

    //Attach Button from the Editor
   // public Button resetButton;
    //GameObject player1 = GameObject.FindWithTag("Player1");
    //GameObject player2 = GameObject.FindWithTag("Player2");
    public float x = 99.0f;
   

    // Start is called before the first frame update
    void Start()
    {
        GameObject player1 = GameObject.FindWithTag(p1);
        GameObject player2 = GameObject.FindWithTag(p2);
        Player_Manager player1Manager = player1.GetComponent<Player_Manager>();
        Player_Manager player2Manager = player2.GetComponent<Player_Manager>();
     
       
        Invoke("ResetScene", x);
     
        
    }
    void ResetScene()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    // Update is called once per frame
    void Update()
    {
        
        
        
    }
  
    
    
}













