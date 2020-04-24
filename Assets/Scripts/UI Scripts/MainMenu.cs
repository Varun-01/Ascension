using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Quit Button Functionality
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
