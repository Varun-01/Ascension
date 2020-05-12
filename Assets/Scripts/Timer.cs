using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    string p1 = "Player1";
    string p2 = "Player2";
    private int pw1;
    private int pw2;
    public float x = 99.0f;
        // Start is called before the first frame update
    void Start()
    {
        GameObject gameTimer = GameObject.Find("ScriptManager");
        Game_Manager mainTimer = gameTimer.GetComponent<Game_Manager>();

        if (mainTimer.ps1 < 2 && mainTimer.ps2 < 2)
        {
            Invoke("ResetScene", x);
        }
        
    }
    void ResetScene()
    {
        GameObject gameTimer = GameObject.Find("ScriptManager");
        Game_Manager mainTimer = gameTimer.GetComponent<Game_Manager>();

        GameObject player1 = GameObject.FindWithTag(p1);
        GameObject player2 = GameObject.FindWithTag(p2);
        Player_Manager player1Manager = player1.GetComponent<Player_Manager>();
        Player_Manager player2Manager = player2.GetComponent<Player_Manager>();
        if (player1Manager.playerHealth > player2Manager.playerHealth)
        {
            mainTimer.ps1++;
            mainTimer.rNum++;
            mainTimer.UILoader(); ;

        }
        if (player2Manager.playerHealth > player1Manager.playerHealth)
        {
            mainTimer.ps2++;
            mainTimer.rNum++;
            mainTimer.UILoader(); ;

        }
        Application.LoadLevel(Application.loadedLevel);
        
    }
    // Update is called once per frame

}
