using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {
	//ConnectionManager cManager = null;
	void Awake() {

		//cManager = new ConnectionManager();
		
		
		gameObject.AddComponent<MessageQueue>();
		gameObject.AddComponent<ConnectionManager>();
		DontDestroyOnLoad(gameObject);
		
		NetworkRequestTable.init();
		NetworkResponseTable.init();
		
		//SpeciesTable.initialize();
	}
	
	// Use this for initialization
	void Start () {
		//SceneManager.LoadScene ("Register");
		ConnectionManager cManager = gameObject.GetComponent<ConnectionManager>();

		if (cManager) {
			cManager.setupSocket();

			StartCoroutine(RequestHeartbeat(1f));
		}
		SceneManager.LoadScene ("Login");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator RequestHeartbeat(float time) {
		yield return new WaitForSeconds(time);

		ConnectionManager cManager = gameObject.GetComponent<ConnectionManager>();

		if (cManager) {
			RequestHeartbeat request = new RequestHeartbeat(); //create new request, which contians Constants.CMSG_HEARTBEAT as request_id
			request.send();  //put it to a Gamepacket
		
			cManager.send(request); // put it in byte[] and send to server.
		}

		StartCoroutine(RequestHeartbeat(1f));
	}
}
