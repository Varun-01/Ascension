using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public enum GameState { INTRO, MAIN_MENU }
    float cP1;
    float cP2;
    string cN1;
    string cN2;
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
    public string music;


    /*
    [FMODUnity.EventRef]
    public string MusicEvent = "";

    FMOD.Studio.EventInstance MusicState;
    */

    //Attach Button from the Editor
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
        ps1 = 0;
        ps2 = 0;
        GameObject player1 = GameObject.FindWithTag(p1);
        GameObject player2 = GameObject.FindWithTag(p2);
        Player_Manager player1Manager = player1.GetComponent<Player_Manager>();
        Player_Manager player2Manager = player2.GetComponent<Player_Manager>();

        if (Constants.USER_ID == 128)
        {
            player1Manager.setControllable();

        } else if (Constants.USER_ID == 129)
        {
            player2Manager.setControllable();
        }

        //player1.GetComponent<Player_Attack>().enabled = false;
        //player1.GetComponent<Player_Movement>().enabled = false;
        //player2.GetComponent<Player_Attack>().enabled = false;
        //player2.GetComponent<Player_Movement>().enabled = false;
        GameObject selectionManagerObj = GameObject.Find("SelectionManager");
        Selection_Manager selectionManager = selectionManagerObj.GetComponent<Selection_Manager>();
        //MusicEvent = mChar.getMusic();

        //MusicState = FMODUnity.RuntimeManager.CreateInstance(MusicEvent);
        //MusicState.start();
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
    void spawn()
    {

    }
  
}











