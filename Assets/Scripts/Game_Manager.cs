using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public int rNum;
    public int  ps2;
    bool reset;
    public string music;


    
    [FMODUnity.EventRef]
    public string MusicEvent = "";

    FMOD.Studio.EventInstance MusicState;
    

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
        rNum = 1;
        UI();
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
        int MusicParams = selectionManager.getMusicParam();

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Playlist", MusicParams);

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
                rNum++;
                Application.LoadLevel(Application.loadedLevel);
                UILoader();

            }
            if (player2Manager.alive == 0)
            {
                ps1++;
                rNum++;
                Application.LoadLevel(Application.loadedLevel);
                UILoader();

            }
        }

    }
    public void UILoader()
    {
        GameObject player1 = GameObject.FindWithTag(p1);
        GameObject player2 = GameObject.FindWithTag(p2);
        //player1.GetComponent<Player_Attack>().enabled = false;
        //player1.GetComponent<Player_Movement>().enabled = false;
        //player2.GetComponent<Player_Attack>().enabled = false;
        //player2.GetComponent<Player_Movement>().enabled = false;
        Invoke("UI", 1);
    }
    void UI()
    {

        GameObject round1 = GameObject.FindWithTag("Round 1");
        GameObject round2 = GameObject.FindWithTag("Round 2");
        GameObject fRound = GameObject.FindWithTag("Final Round");
        GameObject fight = GameObject.FindWithTag("Fight");
        GameObject p1Win = GameObject.FindWithTag("Player1 win");
        GameObject p2Win = GameObject.FindWithTag("Player2 win");
        GameObject p1_1 = GameObject.FindWithTag("p1 1win");
        GameObject p1_2 = GameObject.FindWithTag("p1 2win");
        GameObject p2_1 = GameObject.FindWithTag("p2 1win");
        GameObject p2_2 = GameObject.FindWithTag("p2 2win");
        if (rNum == 1)
        {
            round1.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            Invoke("fightOn", 2);
        }
        if (rNum == 2)
        {
            round2.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            Invoke("fightOn", 2);
        }
        if (rNum == 3 && ps1 != 2 && ps2 != 2)
        {
            fRound.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            Invoke("fightOn", 2);
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("RoundCount", 3);
        }
        if (rNum == 4 || ps1 == 2 || ps2 == 2)
        {
            if (ps1 > ps2)
            {
                p1Win.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
                Invoke("sceneChange", 5);

            }
            if (ps1 < ps2)
            {
                p2Win.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
                Invoke("sceneChange", 5);
            }
        }


        if (ps2 == 1)
        {
            p2_1.GetComponent<Image>().enabled = true;
        }
        if (ps2 == 2)
        {
            p2_1.GetComponent<Image>().enabled = true;
            p2_2.GetComponent<Image>().enabled = true;
        }
        if (ps1 == 1)
        {
            p1_1.GetComponent<Image>().enabled = true;
        }
        if (ps1 == 2)
        {
            p1_1.GetComponent<Image>().enabled = true;
            p1_2.GetComponent<Image>().enabled = true;
        }




    }
    void fightOn()
    {
        GameObject player1 = GameObject.FindWithTag(p1);
        GameObject player2 = GameObject.FindWithTag(p2);
        //player1.GetComponent<Player_Attack>().enabled = true;
        //player1.GetComponent<Player_Movement>().enabled = false;
        //player2.GetComponent<Player_Attack>().enabled = true;
        //player2.GetComponent<Player_Movement>().enabled = false;
        GameObject fight = GameObject.FindWithTag("Fight");
        fight.GetComponent<Image>().enabled = true;
        Invoke("fightOff", 1);
    }
    void fightOff()
    {
        GameObject round1 = GameObject.FindWithTag("Round 1");
        GameObject round2 = GameObject.FindWithTag("Round 2");
        GameObject fRound = GameObject.FindWithTag("Final Round");
        GameObject fight = GameObject.FindWithTag("Fight");
        fight.GetComponent<Image>().enabled = false;
        round1.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
        round2.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
        fRound.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
    }
    void sceneChange()
    {
        SceneManager.LoadScene("Character Select Multiplayer");
    }

}













    




   



