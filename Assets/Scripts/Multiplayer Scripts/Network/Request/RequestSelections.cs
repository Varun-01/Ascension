using UnityEngine;

using System;

public class RequestSelections : NetworkRequest {

	public RequestSelections() {
		request_id = Constants.CMSG_SELECTIONS;
	}
	
	public void send(int selectedCharacterIndex) {
		Debug.Log ("called requestSelection.send() function: "+ request_id);
	    packet = new GamePacket(request_id);
		//packet.addString(Constants.CLIENT_VERSION);
		//packet.addString(attackName);
		packet.addInt32(selectedCharacterIndex);
		//packet.addString(username);
		//packet.addString(password);

	}
}