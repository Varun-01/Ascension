using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Move : MonoBehaviour {
	
	private GameObject mainObject;
	public GameObject opponent;
	 private MessageQueue msgQueue;
	 private ConnectionManager cManager;

	 public Player_Movement opponentMovement;
	 public Player_Attack opponentAttack;

	 Dictionary<string, string> attackKeyTable = new Dictionary<string, string>();
     Dictionary<string, float> movementKeyTable = new Dictionary<string, float>();

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
		if(Constants.USER_ID < Constants.OPPONENT_ID){
		opponent = GameObject.FindWithTag("Player2");}
		else{opponent = GameObject.FindWithTag("Player1");}
		opponentMovement = opponent.GetComponent<Player_Movement>();
		opponentAttack = opponent.GetComponent<Player_Attack>();

		attackKeyTable.Add("U","LightPunch");
		attackKeyTable.Add("I","HeavyPunch");
		attackKeyTable.Add("O","LightKick");
		attackKeyTable.Add("P","HeavyKick");

        movementKeyTable.Add("D", 1);
        movementKeyTable.Add("A", -1);
    }
	
	//Network, entry point function
	public void sendMoveRequest(string key) {
		Debug.Log("Sending move request****************************"+key);
		cManager.send(requestMove(key));  
		//requestLogin is the function in line 78. The function returns a request (type if RequestLogin). 
		 //inside request, there is a packet(type GamePacket), which contains request_id, CLIENT_VERION,username, passowrd.
	} //cManager.send() coverts the request into byte[] and send it to server. 
	
	public RequestMove requestMove(string key) {
		RequestMove request = new RequestMove();
		//if(request != null) {Debug.Log ("request 37 move is NOT null*******");}
		request.send(key);
		Debug.Log ("called requestMove function and send");
		return request;
	}
	
	public void responseMove(ExtendedEventArgs eventArgs) {
		
		ResponseMoveEventArgs args = eventArgs as ResponseMoveEventArgs;
		if (args.status == 0) {
			//Constants.USER_ID = args.user_id;
			Debug.Log ("Successful Move response : ");
			//EditorUtility.DisplayDialog ("Move Successful:"+args.key, "You have successfully move.\nClick Ok to continue execution and see responses on console", "Ok");
            //SceneManager.LoadScene("Main Menu");
			
			//@todo it should not be an if else relationship
			// if(attackKeyTable.ContainsKey(args.key)){
			// opponentAttack.launchAttackFromNet(attackKeyTable[args.key]);}
			// else{
			// opponentMovement.checkMovement(1,-95);}

			if(args.user_id == Constants.OPPONENT_ID){
		
			if(attackKeyTable.ContainsKey(args.key)){
			opponentAttack.launchAttackFromNet(attackKeyTable[args.key]);}
			else{
					if(movementKeyTable.ContainsKey(args.key)){
                    opponentMovement.checkMovement(movementKeyTable[args.key]);
					}
                }
				if(args.key=="Jump"){
						opponentMovement.jump();
						}
			}
				
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

	void FixedUpdate(){
		opponentMovement.completeJump();
	}
}
