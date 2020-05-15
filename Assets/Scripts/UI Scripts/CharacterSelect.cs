using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    private int selectedCharacterIndex;
    private Color desiredColor;

    [Header("List of Characters")]
    [SerializeField] private List<CharacterSelectObject> characterList = new List<CharacterSelectObject>();


    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterSplash;
    [SerializeField] private Image backgroundColor;

    [Header("Sounds")]
    [SerializeField] private AudioClip arrowClickSFX;
    [SerializeField] private AudioClip characterSelectMusic;

    public GameObject selection;
    public Selection_Manager selectionManager;

    public Selections selectionsFromNetwork;

    public void LeftArrow()
    {
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
        {
            selectedCharacterIndex = characterList.Count - 1;
        }

        UpdateCharacterSelectionUI();
    }

    public void RightArrow()
    {
        selectedCharacterIndex++;
        if (selectedCharacterIndex == characterList.Count)
        {
            selectedCharacterIndex = 0;
        }

        UpdateCharacterSelectionUI();
    }

    public void Select()
    { 
        if(Constants.USER_ID <= Constants.OPPONENT_ID){
            Debug.Log("<=setLocal Player1 selectedCharacterIndex: "+ selectedCharacterIndex);
            selectionManager.setCharacter1(string.Format(characterList[selectedCharacterIndex].characterName));
        }else{
            Debug.Log("<=setLocal Player2 selectedCharacterIndex: "+ selectedCharacterIndex);
           selectionManager.setCharacter2(string.Format(characterList[selectedCharacterIndex].characterName)); 
        }
        selectionsFromNetwork.sendSelectionsRequest(selectedCharacterIndex);
        SceneManager.LoadScene("StageSelect"); 
        //int count = 0;
        //selectionManager.setCharacter1(string.Format(characterList[selectedCharacterIndex].characterName));
        //Debug.Log(string.Format("Character {0}:{1} has been selected by Player 1", selectedCharacterIndex, characterList[selectedCharacterIndex].characterName));
        // while(count<1){
        // if(Constants.USER_ID <= Constants.OPPONENT_ID){
        //     Debug.Log("called <");
        // if(count ==0 ){
        // selectionManager.setCharacter1(string.Format(characterList[selectedCharacterIndex].characterName));
        // count++;
        // }else{
        // selectionManager.setCharacter2(string.Format(characterList[selectedCharacterIndex].characterName));
        // }
        // }else{
        // if(count==0){
        // selectionManager.setCharacter1(string.Format(characterList[selectedCharacterIndex].characterName));   
        // count++; 
        // }else{
        // selectionManager.setCharacter2(string.Format(characterList[selectedCharacterIndex].characterName));
        // Debug.Log("called > "+selectionManager.getCharacter2());}
        // }
        // }
    }

    public void selectForNetwork(int networkSelectedCharacterIndex){
        if(Constants.USER_ID <= Constants.OPPONENT_ID){
            Debug.Log("<=setNetwork Player2 networkSelectedCharacterIndex: "+ networkSelectedCharacterIndex);
            selectionManager.setCharacter2(string.Format(characterList[networkSelectedCharacterIndex].characterName));
        }else{
            Debug.Log(">setNetwork Player1 networkSelectedCharacterIndex: "+ networkSelectedCharacterIndex);
           selectionManager.setCharacter1(string.Format(characterList[networkSelectedCharacterIndex].characterName)); 
        }

    }

    private void UpdateCharacterSelectionUI()
    {
        //Splash, Name, Desired Color
        characterSplash.sprite = characterList[selectedCharacterIndex].splash;
        characterName.text = characterList[selectedCharacterIndex].characterName;
        desiredColor = characterList[selectedCharacterIndex].characterBGColor;
    }

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite splash;
        public string characterName;
        public Color characterBGColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateCharacterSelectionUI();
        selection = GameObject.Find("SelectionManager");
        selectionManager = selection.GetComponent<Selection_Manager>();
        DontDestroyOnLoad(gameObject);
        //update playerprefs to determine previous scene
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastScene", currentScene);
        PlayerPrefs.Save();
        selectionsFromNetwork = gameObject.GetComponent<Selections>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
