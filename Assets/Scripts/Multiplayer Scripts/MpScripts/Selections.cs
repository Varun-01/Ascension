using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEditor;

public class Selections : MonoBehaviour {
	
	private GameObject mainObject;
	 private MessageQueue msgQueue;
	 private ConnectionManager cManager;

	GameObject characterSelectionControllerObj;
    public Player_Manager opponentManager;
	public CharacterSelect characterSelect;

	public int attackStat;
    public int defenseStat;
    public string characterName;
    public string playerTag;
	private int selectedStageIndex=1;
	private int selectedCharacterIndex=1;
	private int selectedMusicIndex=1;
	
	void Awake() {
		characterSelectionControllerObj = GameObject.Find("CharacterSelectionController1");
		mainObject = GameObject.Find("MainObject");

		characterSelect = characterSelectionControllerObj.GetComponent<CharacterSelect>();
		//DontDestroyOnLoad(mainObject);
		//Debug.Log(mainObject != null? "AddMONEYmainObject is not null" : "mainObject is null");
		cManager = mainObject.GetComponent<ConnectionManager>();
		msgQueue = mainObject.GetComponent<MessageQueue>();
		msgQueue.AddCallback(Constants.SMSG_SELECTIONS, responseSelections);

	}
	
	// Use this for initialization
	void Start() {
	
	}
	
	//Network, entry point function
	public void sendSelectionsRequest(int selectedCharacterIndex) {
		Debug.Log("Sending Selection request****************************");
		cManager.send(requestSelections(selectedCharacterIndex));  
		//requestLogin is the function in line 78. The function returns a request (type if RequestLogin). 
		 //inside request, there is a packet(type GamePacket), which contains request_id, CLIENT_VERION,username, passowrd.
	} //cManager.send() coverts the request into byte[] and send it to server. 
	
	public RequestSelections requestSelections(int selectedCharacterIndex) {
		RequestSelections request = new RequestSelections();
		//if(request != null) {Debug.Log ("request 52 Attack is NOT null*******");}
		//Debug.Log("Damage is !!!!!!!!!!!!!!"+damage);
		request.send(selectedCharacterIndex);
		Debug.Log ("called requestSelections function and send");
		return request;
	}
	
	public void responseSelections(ExtendedEventArgs eventArgs) {
		
		ResponseSelectionsEventArgs args = eventArgs as ResponseSelectionsEventArgs;
		if (args.status == 0) {
			//Constants.USER_ID = args.user_id;
			Debug.Log ("Successful select response : " +args.selectedCharacterIndex);
			//EditorUtility.DisplayDialog ("Attack Successful: "+args.damage, "You have successfully attack.\nClick Ok to continue execution and see responses on console", "Ok");
            if(args.user_id != Constants.USER_ID){
			characterSelect.selectForNetwork(args.selectedCharacterIndex);}
			//characterSelect.Select();
			//SceneManager.LoadScene("Main Menu");
		} else {
			Debug.Log("Attack Failed");
		}
	}
	

	public void Show() {
		//isHidden = false;
	}
	
	public void Hide() {
		//isHidden = true;
	}
	
	// Update is called once per frame
	void Update() {
	}
}
