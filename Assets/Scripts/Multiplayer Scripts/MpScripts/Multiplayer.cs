using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEditor;

public class Multiplayer : MonoBehaviour {
	
	private GameObject mainObject;

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
			EditorUtility.DisplayDialog ("Matching Successful", "Your Opponent: "+args.opponent_id, "Ok");
            SceneManager.LoadScene("VS_Testing");
		} else {
			Debug.Log("Match Failed");
		}
	}
	

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
