using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEditor;

public class Multiplayer : MonoBehaviour {
	
	private GameObject mainObject;
	// Window Properties
	private float width = 280;
	private float height = 100;
	// Other
	public Texture background;
	private string user_id = "";
	private string password = "";
	private Rect windowRect;
	private bool isHidden;
	private MessageQueue msgQueue;
	private ConnectionManager cManager;
	
	void Awake() {
		
		mainObject = GameObject.Find("MainObject");
		DontDestroyOnLoad(mainObject);
		cManager = mainObject.GetComponent<ConnectionManager>();
		msgQueue = mainObject.GetComponent<MessageQueue> ();
		msgQueue.AddCallback(Constants.SMSG_MATCH_PLAYER, ResponseMatchPlayer);
		// msgQueue.AddCallback(Constants.SMSG_PLAYERS, responsePlayers);
		// msgQueue.AddCallback (Constants.SMSG_TEST, responseTest);
	}
	
	// Use this for initialization
	void Start() {

	}
	
	// void OnGUI() {
	// 	// Background
	// 	GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
	// 	// Client Version Label
		
	// 	// Login Interface
	// 	if (!isHidden) {
	// 		windowRect = new Rect ((Screen.width - width) / 2, Screen.height / 2 - height, width, height);
	// 		windowRect = GUILayout.Window((int) Constants.GUI_ID.Login, windowRect, MakeWindow, "Login");
	// 		if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Return) {
	// 			Submit();
	// 		}
	// 	}
	// }
	
	// void MakeWindow(int id) {
	// 	GUILayout.Label("User ID");
	// 	GUI.SetNextControlName("username_field");
	// 	user_id = GUI.TextField(new Rect(10, 45, windowRect.width - 20, 25), user_id, 25);
	// 	GUILayout.Space(30);
	// 	GUILayout.Label("Password");
	// 	GUI.SetNextControlName("password_field");
	// 	password = GUI.PasswordField(new Rect(10, 100, windowRect.width - 20, 25), password, "*"[0], 25);
	// 	GUILayout.Space(75);
	// 	if (GUI.Button(new Rect(windowRect.width / 2 - 50, 135, 100, 30), "Match Player")) {
	// 		Submit();
	// 	}
	// }
	
	public void Submit() {
	
			cManager.send(requestMatchPlayer());  //requestLogin is the function in line 78. The function returns a request (type is RequestLogin). 
	   //inside request, there is a packet(type GamePacket), which contains request_id, CLIENT_VERION,username, passowrd.
	} //cManager.send() coverts the request into byte[] and send it to server. 
	
	public RequestMatchPlayer requestMatchPlayer() {
		RequestMatchPlayer request = new RequestMatchPlayer();
		request.send();
		return request;
	}
	
	public void ResponseMatchPlayer(ExtendedEventArgs eventArgs) {
		ResponseMatchPlayerEventArgs args = eventArgs as ResponseMatchPlayerEventArgs;
		if (args.status == 0) {
			Constants.USER_ID = args.user_id;
			Debug.Log ("Successful Matchplay response : " + args.ToString());
			EditorUtility.DisplayDialog ("Matching Successful", "Opponent: "+args.opponent_id, "Ok");
            //SceneManager.LoadScene("TestScene");
		} else {
			Debug.Log("Match Failed");
		}
	}
	
	// public RequestPlayers requestPlayers() {
	// 	RequestPlayers request = new RequestPlayers();
	// 	request.send ();
	// 	return request;
	// }

	// public void responsePlayers(ExtendedEventArgs eventArgs) {
	// 	ResponsePlayersEventArgs args = eventArgs as ResponsePlayersEventArgs;
	// 	int numActivePlayers = args.numActivePlayers;
	// 	Debug.Log ("Number of Connected Players : " + numActivePlayers);
	// }

	// 	public RequestTest requestTest(string arithmeticOperator, int testNum) {
	// 	RequestTest requestTest = new RequestTest ();
	// 	requestTest.send (arithmeticOperator, testNum);
	// 	return requestTest;
	// }
	
	// public void responseTest(ExtendedEventArgs eventArgs) {
	// 	ResponseTestEventArgs args = eventArgs as ResponseTestEventArgs;
	// 	Debug.Log ("newTestVar updated on server!!!");
	// }

	public void Show() {
		isHidden = false;
	}
	
	public void Hide() {
		isHidden = true;
	}
	
	// Update is called once per frame
	void Update() {
	}
}
