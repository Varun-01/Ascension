using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public enum GameState { INTRO, MAIN_MENU }

    GameObject player1 = GameObject.FindWithTag("Player1");
    GameObject player2 = GameObject.FindWithTag("Player2");

   

    // Start is called before the first frame update
    void Start()
    {
        Player_Manager player1Manager = player1.GetComponent<Player_Manager>();
        Player_Manager player2Manager = player2.GetComponent<Player_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
