using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectButtons : MonoBehaviour
{
    public void backButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void fightButton()
    {
        SceneManager.LoadScene("StageSelect");
    }
}
