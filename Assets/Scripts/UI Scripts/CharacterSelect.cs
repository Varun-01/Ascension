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
        //Debug.Log(string.Format("Character {0}:{1} has been selected by Player 1", selectedCharacterIndex, characterList[selectedCharacterIndex].characterName));
        selectionManager.setCharacter1(string.Format(characterList[selectedCharacterIndex].characterName));
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

        //update playerprefs to determine previous scene
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastScene", currentScene);
        PlayerPrefs.Save();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
