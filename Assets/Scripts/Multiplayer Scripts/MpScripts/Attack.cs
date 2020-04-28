using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEditor;

public class Attack : MonoBehaviour {
	
	private GameObject mainObject, attackObject;
	// Window Properties
	// private float width = 280;
	// private float height = 100;
	// // Other
	// public Texture background;
	//private string user_id = "";
	//private string password = "";
	// private Rect windowRect;
	// private bool isHidden;
	 private MessageQueue msgQueue;
	 private ConnectionManager cManager;
	
	void Awake() {
		
		mainObject = GameObject.Find("MainObject");
		//DontDestroyOnLoad(mainObject);
		//Debug.Log(mainObject != null? "AddMONEYmainObject is not null" : "mainObject is null");
		cManager = mainObject.GetComponent<ConnectionManager>();
		msgQueue = mainObject.GetComponent<MessageQueue> ();

		//Debug.Log(responseAttack != null? "responseAttack is not null" : "responseAttack is null");
		msgQueue.AddCallback(Constants.SMSG_ATT, ResponseAttack);

		//msgQueue.AddCallback(Constants.SMSG_PLAYERS, responsePlayers);
		//msgQueue.AddCallback (Constants.SMSG_TEST, responseTest);
	}
	
	// Use this for initialization
	void Start() {

	}
	
	//Network, entry point function
	public void sendAttackRequest(int attackStat, int damage) {
		Debug.Log("Sending attack request...");
		//int moneyToAdd = 5;
	
		cManager.send(requestAttack(attackStat,damage));  
		//requestLogin is the function in line 78. The function returns a request (type if RequestLogin). 
		 //inside request, there is a packet(type GamePacket), which contains request_id, CLIENT_VERION,username, passowrd.
	} //cManager.send() coverts the request into byte[] and send it to server. 
	
	public RequestAttack requestAttack(int attackStat, int damage) {
		RequestAttack request = new RequestAttack();
		if(request != null) {Debug.Log ("request 52 Attack is NOT null*******");}
		request.send(attackStat,damage);
		Debug.Log ("called requestAttack function and send");
		return request;
	}
	
	public void ResponseAttack(ExtendedEventArgs eventArgs) {
		
		ResponseAttackEventArgs args = eventArgs as ResponseAttackEventArgs;
		if (args.status == 0) {
			Constants.USER_ID = args.user_id;
			Debug.Log ("Successful attack response : ");
			EditorUtility.DisplayDialog ("Attack Successful", "You have successfully attack.\nClick Ok to continue execution and see responses on console", "Ok");
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
