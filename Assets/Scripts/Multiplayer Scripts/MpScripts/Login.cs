using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEditor;

public class Login : MonoBehaviour {
	
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

	public int play_id;
	
	void Awake() {
		
		mainObject = GameObject.Find("MainObject");
		DontDestroyOnLoad(mainObject);
		cManager = mainObject.GetComponent<ConnectionManager>();
		msgQueue = mainObject.GetComponent<MessageQueue> ();
		msgQueue.AddCallback(Constants.SMSG_AUTH, responseLogin);
		// msgQueue.AddCallback(Constants.SMSG_PLAYERS, responsePlayers);
		// msgQueue.AddCallback (Constants.SMSG_TEST, responseTest);
	}
	
	// Use this for initialization
	void Start() {

	}
	
	void OnGUI() {
		// Background
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
		// Client Version Label
		
		// Login Interface
		if (!isHidden) {
			windowRect = new Rect ((Screen.width - width) / 2, Screen.height / 2 - height, width, height);
			windowRect = GUILayout.Window((int) Constants.GUI_ID.Login, windowRect, MakeWindow, "Login");
			if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Return) {
				Submit();
			}
		}
	}
	
	void MakeWindow(int id) {
		GUILayout.Label("User ID");
		GUI.SetNextControlName("username_field");
		user_id = GUI.TextField(new Rect(10, 45, windowRect.width - 20, 25), user_id, 25);
		GUILayout.Space(30);
		GUILayout.Label("Password");
		GUI.SetNextControlName("password_field");
		password = GUI.PasswordField(new Rect(10, 100, windowRect.width - 20, 25), password, "*"[0], 25);
		GUILayout.Space(75);
		if (GUI.Button(new Rect(windowRect.width / 2 - 50, 135, 100, 30), "Log In")) {
			Submit();
		}
	}
	
	public void Submit() {
		user_id = user_id.Trim();
		password = password.Trim();
		if (user_id.Length == 0) {
			Debug.Log("User ID Required");
			GUI.FocusControl("username_field");
		} else if (password.Length == 0) {
			Debug.Log("Password Required");
			GUI.FocusControl("password_field");
		} else {
			cManager.send(requestLogin(user_id, password));  //requestLogin is the function in line 78. The function returns a request (type is RequestLogin). 
		} //inside request, there is a packet(type GamePacket), which contains request_id, CLIENT_VERION,username, passowrd.
	} //cManager.send() coverts the request into byte[] and send it to server. 
	
	public RequestLogin requestLogin(string username, string password) {
		RequestLogin request = new RequestLogin();
		request.send(username, password);
		return request;
	}
	
	public void responseLogin(ExtendedEventArgs eventArgs) {
		ResponseLoginEventArgs args = eventArgs as ResponseLoginEventArgs;
		if (args.status == 0) {
			Constants.USER_ID = args.user_id;
			Debug.Log ("Successful Login response : " + args.ToString());
			EditorUtility.DisplayDialog ("Login Successful", "You have successfully logged in.\nClick Ok to continue execution and see responses on console", "Ok");
            SceneManager.LoadScene("Main Menu");
		} else {
			Debug.Log("Login Failed");
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
