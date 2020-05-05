using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
        selection = GameObject.Find("SelectionManager");
        selectionManager = selection.GetComponent<Selection_Manager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
