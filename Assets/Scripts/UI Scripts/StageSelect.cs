using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    private int selectedStageIndex;
    private Color desiredColor;



    [Header("List of Stages")]
    [SerializeField] private List<StageSelectObject> stageList = new List<StageSelectObject>();


    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI stageName;
    [SerializeField] private Image stageSplash;
    [SerializeField] private Image backgroundColor;

    [Header("Sounds")]
    [SerializeField] private AudioClip arrowClickSFX;
    [SerializeField] private AudioClip characterSelectMusic;

    public GameObject selection;
    public Selection_Manager selectionManager;

    public void LeftArrow()
    {
        selectedStageIndex--;
        if (selectedStageIndex < 0)
        {
            selectedStageIndex = stageList.Count - 1;
        }

        UpdateStageSelectionUI();
    }

    public void RightArrow()
    {
        selectedStageIndex++;
        if (selectedStageIndex == stageList.Count)
        {
            selectedStageIndex = 0;
        }

        UpdateStageSelectionUI();
    }

    public void Select()
    {
        //Debug.Log(string.Format("Stage {0}:{1} has been selected", selectedStageIndex, stageList[selectedStageIndex].stageName));
        selectionManager.setStage(string.Format(stageList[selectedStageIndex].stageName));
        SceneManager.LoadScene("Music Select");
    }

    public void Back()
    {
        //use PlayerPrefs to determine if player came from Local/Multiplayer Character Select Screen
        string lastScene = PlayerPrefs.GetString("LastScene", null);
        if (lastScene != null)
        {
            if (lastScene == "Character Select Multiplayer")
            {
                SceneManager.LoadScene("Character Select Multiplayer");
            } else
            {
                SceneManager.LoadScene("Character Select");
            }
        }
        //SceneManager.LoadScene("Character Select");
    }
    private void UpdateStageSelectionUI()
    {
        //Splash, Name, Desired Color
        stageSplash.sprite = stageList[selectedStageIndex].splash;
        stageName.text = stageList[selectedStageIndex].stageName;
        desiredColor = stageList[selectedStageIndex].stageBGColor;
    }

    [System.Serializable]
    public class StageSelectObject
    {
        public Sprite splash;
        public string stageName;
        public Color stageBGColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateStageSelectionUI();
        selection = GameObject.Find("SelectionManager");
        selectionManager = selection.GetComponent<Selection_Manager>();

        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
