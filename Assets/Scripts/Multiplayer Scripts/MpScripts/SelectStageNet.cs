using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEditor;

public class SelectStageNet : MonoBehaviour {
	
	private GameObject mainObject;
	 private MessageQueue msgQueue;
	 private ConnectionManager cManager;

	GameObject stageSelectionControllerObj;
    public Player_Manager opponentManager;
	public StageSelect stageSelect;

	public int attackStat;
    public int defenseStat;
    public string characterName;
    public string playerTag;
	private int selectedStageIndex=1;
	private int selectedCharacterIndex=1;
	private int selectedMusicIndex=1;
	
	void Awake() {
		stageSelectionControllerObj = GameObject.Find("StageSelectionController");
		mainObject = GameObject.Find("MainObject");

		stageSelect = stageSelectionControllerObj.GetComponent<StageSelect>();
		//DontDestroyOnLoad(mainObject);
		//Debug.Log(mainObject != null? "AddMONEYmainObject is not null" : "mainObject is null");
		cManager = mainObject.GetComponent<ConnectionManager>();
		msgQueue = mainObject.GetComponent<MessageQueue>();
		msgQueue.AddCallback(Constants.SMSG_SELECTSTAGE, responseSelectStage);

	}
	
	// Use this for initialization
	void Start() {
	
	}
	
	//Network, entry point function
	public void sendStageSelectRequest(int selectedStageIndex) {
		Debug.Log("Sending Stage Selection request****************************");
		cManager.send(requestSelectStage(selectedStageIndex));  
		//requestLogin is the function in line 78. The function returns a request (type if RequestLogin). 
		 //inside request, there is a packet(type GamePacket), which contains request_id, CLIENT_VERION,username, passowrd.
	} //cManager.send() coverts the request into byte[] and send it to server. 
	
	public RequestSelectStage requestSelectStage(int selectedStageIndex) {
		RequestSelectStage request = new RequestSelectStage();
		//if(request != null) {Debug.Log ("request 52 Attack is NOT null*******");}
		//Debug.Log("Damage is !!!!!!!!!!!!!!"+damage);
		request.send(selectedStageIndex);
		Debug.Log ("called requestSelectStage function and send");
		return request;
	}
	
	public void responseSelectStage(ExtendedEventArgs eventArgs) {
		
		ResponseSelectStageEventArgs args = eventArgs as ResponseSelectStageEventArgs;
		if (args.status == 0) {
			//Constants.USER_ID = args.user_id;
			Debug.Log ("Successful select Stage response : " +args.selectedStageIndex);
			//EditorUtility.DisplayDialog ("Music Successful: "+args.selectedStageIndex, "You have successfully attack.\nClick Ok to continue execution and see responses on console", "Ok");
            if(args.user_id  < Constants.USER_ID){

			stageSelect.selectForNetwork(args.selectedStageIndex);}
			//characterSelect.Select();
			//SceneManager.LoadScene("Music Select");
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
