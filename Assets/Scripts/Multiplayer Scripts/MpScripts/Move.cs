using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEditor;

public class Move : MonoBehaviour {
	
	private GameObject mainObject;
	 private MessageQueue msgQueue;
	 private ConnectionManager cManager;
	
	void Awake() {
		
		mainObject = GameObject.Find("MainObject");
		//DontDestroyOnLoad(mainObject);
		//Debug.Log(mainObject != null? "AddMONEYmainObject is not null" : "mainObject is null");
		cManager = mainObject.GetComponent<ConnectionManager>();
		msgQueue = mainObject.GetComponent<MessageQueue> ();
		msgQueue.AddCallback(Constants.SMSG_MOVE, responseMove);
	}
	
	// Use this for initialization
	void Start() {

	}
	
	//Network, entry point function
	public void sendAttackRequest(string key) {
		Debug.Log("Sending move request****************************");
		cManager.send(requestMove(key));  
		//requestLogin is the function in line 78. The function returns a request (type if RequestLogin). 
		 //inside request, there is a packet(type GamePacket), which contains request_id, CLIENT_VERION,username, passowrd.
	} //cManager.send() coverts the request into byte[] and send it to server. 
	
	public RequestMove requestMove(string key) {
		RequestMove request = new RequestMove();
		if(request != null) {Debug.Log ("request 37 move is NOT null*******");}
		request.send(key);
		Debug.Log ("called requestMove function and send");
		return request;
	}
	
	public void responseMove(ExtendedEventArgs eventArgs) {
		
		ResponseMoveEventArgs args = eventArgs as ResponseMoveEventArgs;
		if (args.status == 0) {
			Constants.USER_ID = args.user_id;
			Debug.Log ("Successful attack response : ");
			EditorUtility.DisplayDialog ("Move Successful", "You have successfully move.\nClick Ok to continue execution and see responses on console", "Ok");
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
