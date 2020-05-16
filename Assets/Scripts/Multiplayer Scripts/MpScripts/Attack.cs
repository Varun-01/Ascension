using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEditor;

public class Attack : MonoBehaviour {
	
	private GameObject mainObject;
	 private MessageQueue msgQueue;
	 private ConnectionManager cManager;

	GameObject opponent;
	GameObject thisPlayer;
    public Player_Manager opponentManager;
	public Player_Manager thisPlayerManager;

	public int attackStat;
    public int defenseStat;
    public string characterName;
    public string playerTag;
	
	void Awake() {
		
		mainObject = GameObject.Find("MainObject");
		//DontDestroyOnLoad(mainObject);
		//Debug.Log(mainObject != null? "AddMONEYmainObject is not null" : "mainObject is null");
		cManager = mainObject.GetComponent<ConnectionManager>();
		msgQueue = mainObject.GetComponent<MessageQueue> ();
		msgQueue.AddCallback(Constants.SMSG_ATT, responseAttack);
	}
	
	// Use this for initialization
	void Start() {
		//opponent = GameObject.FindWithTag("Player1");

		if(Constants.USER_ID < Constants.OPPONENT_ID){
		opponent = GameObject.FindWithTag("Player1");
		thisPlayer = GameObject.FindWithTag("Player2");
		}
		else{opponent = GameObject.FindWithTag("Player2");
		thisPlayer = GameObject.FindWithTag("Player1");
		}

		opponentManager = opponent.GetComponent<Player_Manager>();
		thisPlayerManager = opponent.GetComponent<Player_Manager>();

		playerTag = gameObject.tag;
        Player_Stats playerStats = gameObject.GetComponent<Player_Stats>();
        characterName = gameObject.name;
        //Debug.Log(characterName);
        attackStat = playerStats.getPlayerAttack(characterName);
	}
	
	//Network, entry point function
	public void sendAttackRequest(int damage) {
		Debug.Log("Sending attack request****************************");
		cManager.send(requestAttack(damage));  
		//requestLogin is the function in line 78. The function returns a request (type if RequestLogin). 
		 //inside request, there is a packet(type GamePacket), which contains request_id, CLIENT_VERION,username, passowrd.
	} //cManager.send() coverts the request into byte[] and send it to server. 
	
	public RequestAttack requestAttack(int damage) {
		RequestAttack request = new RequestAttack();
		//if(request != null) {Debug.Log ("request 52 Attack is NOT null*******");}
		//Debug.Log("Damage is !!!!!!!!!!!!!!"+damage);
		request.send(damage);
		Debug.Log ("called requestAttack function and send");
		return request;
	}
	
	public void responseAttack(ExtendedEventArgs eventArgs) {
		
		ResponseAttackEventArgs args = eventArgs as ResponseAttackEventArgs;
		if (args.status == 0) {
			//Constants.USER_ID = args.user_id;
			Debug.Log ("Successful attack response : " +args.damage);
			//EditorUtility.DisplayDialog ("Attack Successful: "+args.damage, "You have successfully attack.\nClick Ok to continue execution and see responses on console", "Ok");
            if(args.user_id == Constants.OPPONENT_ID){
			thisPlayerManager.TakeDamage(args.damage + attackStat);}
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
