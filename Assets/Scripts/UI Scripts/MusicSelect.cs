using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MusicSelect : MonoBehaviour
{
    private int selectedMusicIndex;
    private Color desiredColor;

    [Header("List of Tracks")]
    [SerializeField] private List<MusicSelectObject> trackList = new List<MusicSelectObject>();


    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI trackName;
    [SerializeField] private Image musicSplash;
    [SerializeField] private Image backgroundColor;

    [Header("Sounds")]
    [SerializeField] private AudioClip arrowClickSFX;
    [SerializeField] private AudioClip characterSelectMusic;

    public GameObject selection;
    public Selection_Manager selectionManager;

    SelectMusicNet selectMusicNet;

    public void Awake(){
         selection = GameObject.Find("SelectionManager");
        selectionManager = selection.GetComponent<Selection_Manager>();
    }

    public void LeftArrow()
    {
        selectedMusicIndex--;
        if (selectedMusicIndex < 0)
        {
            selectedMusicIndex = trackList.Count - 1;
        }

        UpdateMusicSelectionUI();
    }

    public void RightArrow()
    {
        selectedMusicIndex++;
        if (selectedMusicIndex == trackList.Count)
        {
            selectedMusicIndex = 0;
        }

        UpdateMusicSelectionUI();
    }

    public void Select()
    {
        //Debug.Log(string.Format("Track {0}:{1} has been selected", selectedMusicIndex, trackList[selectedMusicIndex].trackName));
        if(Constants.USER_ID < Constants.OPPONENT_ID){
        selectMusicNet.sendMusicSelectRequest(selectedMusicIndex);
        selectionManager.setMusic(string.Format(trackList[selectedMusicIndex].trackName));
        string stage = selectionManager.getStage();
        SceneManager.LoadScene(stage); //load game scene here
        }
        else{
            EditorUtility.DisplayDialog ("Please wait for your opponent to selct the Music ", "Waitting ....", "Ok");
        }
    }

    public void selectForNetwork (int selectedMusicIndex)
    {   Debug.Log("called Music selectForNetwork"+ selectedMusicIndex);
         selectionManager.setMusic(string.Format(trackList[selectedMusicIndex].trackName));
         string stage = selectionManager.getStage();
        SceneManager.LoadScene(stage); //load game scene here
    }

    public void Back()
    {
        SceneManager.LoadScene("StageSelect");
    }
    private void UpdateMusicSelectionUI()
    {
        //Splash, Name, Desired Color
        musicSplash.sprite = trackList[selectedMusicIndex].splash;
        trackName.text = trackList[selectedMusicIndex].trackName;
        desiredColor = trackList[selectedMusicIndex].musicBGColor;
    }

    [System.Serializable]
    public class MusicSelectObject
    {
        public Sprite splash;
        public string trackName;
        public Color musicBGColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateMusicSelectionUI();
       
        //Debug.Log(selection != null? "selection in MusicSelect is not null" : "selection in MusicSelect is null");
        
        selectMusicNet = gameObject.GetComponent<SelectMusicNet>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
