using UnityEngine;

using System;

public class RequestMove : NetworkRequest {

	public RequestMove() {
		request_id = Constants.CMSG_MOVE;
	}
	
	public void send(string key) {
		Debug.Log ("called requestMove.send() function: "+ request_id);
	    packet = new GamePacket(request_id);
		//packet.addString(Constants.CLIENT_VERSION);
		//packet.addInt32(moneyToAdd);
		packet.addString(key);
		//packet.addString(password);

	}
}