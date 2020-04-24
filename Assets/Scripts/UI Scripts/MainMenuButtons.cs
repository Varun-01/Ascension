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

}