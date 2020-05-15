using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void Training()
    {
        SceneManager.LoadScene("Character Select");
    }

    public void LocalMatch()
    {
        SceneManager.LoadScene("Character Select");
    }

    public void RandomMatch()
    {

    }

    public void Connect()
    {
        SceneManager.LoadScene("Connect Menu");
    }

    public void ConnectBack()
    {
        SceneManager.LoadScene("Main Menu");
    }

}