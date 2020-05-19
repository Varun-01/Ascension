using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection_Manager : MonoBehaviour
{
    public string character1 = "UnityChan";
    public string character2 = "UnityChan Alt";
    public string stage;
    public string music;
    public int musicParam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCharacter1(string characterName) {
        character1 = characterName;
    }

    public void setCharacter2(string characterName)
    {
        character2 = characterName;
        Debug.Log(characterName);
    }

    public void setStage(string stageName)
    {
        stage = stageName;
    }

    public void setMusic(string musicName)
    {
        music = musicName;
    }

    public void setMusicParam(int musicParamChosen)
    {
        musicParam = musicParamChosen;
    }

    public string getCharacter1()
    {
        return character1;
    }

    public string getCharacter2()
    {
        return character2;
    }

    public string getStage()
    {
        return stage;
    }

    public string getMusic()
    {
        return music;
    }

    public int getMusicParam()
    {
        return musicParam;
    }
}
