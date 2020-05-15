using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEditor;

public class SelectMusicNet : MonoBehaviour {
	
	private GameObject mainObject;
	 private MessageQueue msgQueue;
	 private ConnectionManager cManager;

	GameObject musicSelectionControllerOBj;
    public Player_Manager opponentManager;
	public MusicSelect musicSelect;

	public int attackStat;
    public int defenseStat;
    public string characterName;
    public string playerTag;
	private int selectedStageIndex=1;
	private int selectedCharacterIndex=1;
	private int selectedMusicIndex=1;
	
	void Awake() {
		musicSelectionControllerOBj = GameObject.Find("MusicSelectionController");
		mainObject = GameObject.Find("MainObject");

		musicSelect = musicSelectionControllerOBj.GetComponent<MusicSelect>();
		//DontDestroyOnLoad(mainObject);
		//Debug.Log(mainObject != null? "AddMONEYmainObject is not null" : "mainObject is null");
		cManager = mainObject.GetComponent<ConnectionManager>();
		msgQueue = mainObject.GetComponent<MessageQueue>();
		msgQueue.AddCallback(Constants.SMSG_SELECTMUSIC, responseSelectMusic);

	}
	
	// Use this for initialization
	void Start() {
	
	}
	
	//Network, entry point function
	public void sendMusicSelectRequest(int selectedMusicIndex) {
		Debug.Log("Sending Music Selection request****************************");
		cManager.send(requestSelectMusic(selectedMusicIndex));  
		//requestLogin is the function in line 78. The function returns a request (type if RequestLogin). 
		 //inside request, there is a packet(type GamePacket), which contains request_id, CLIENT_VERION,username, passowrd.
	} //cManager.send() coverts the request into byte[] and send it to server. 
	
	public RequestSelectMusic requestSelectMusic(int selectedMusicIndex) {
		RequestSelectMusic request = new RequestSelectMusic();
		//if(request != null) {Debug.Log ("request 52 Attack is NOT null*******");}
		//Debug.Log("Damage is !!!!!!!!!!!!!!"+damage);
		request.send(selectedMusicIndex);
		Debug.Log ("called requestSelectMusic function and send");
		return request;
	}
	
	public void responseSelectMusic(ExtendedEventArgs eventArgs) {
		
		ResponseSelectMusicEventArgs args = eventArgs as ResponseSelectMusicEventArgs;
		if (args.status == 0) {
			//Constants.USER_ID = args.user_id;
			Debug.Log ("Successful select Music response : " +args.selectedMusicIndex);
			//EditorUtility.DisplayDialog ("Music Successful: "+args.selectedMusicIndex, "You have successfully attack.\nClick Ok to continue execution and see responses on console", "Ok");
            if(args.user_id  < Constants.USER_ID){
			musicSelect.selectForNetwork(args.selectedMusicIndex);}
			//characterSelect.Select();
			//SceneManager.LoadScene("Stage");
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
