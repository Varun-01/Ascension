using UnityEngine;

using System;

public class RequestAttack : NetworkRequest {

	public RequestAttack() {
		request_id = Constants.CMSG_ATT;
	}
	
	public void send(string attackName, int damage) {
		Debug.Log ("called requestAttack.send() function: "+ request_id);
	    packet = new GamePacket(request_id);
		packet.addString(Constants.CLIENT_VERSION);
		packet.addString(attackName);
		packet.addInt32(damage);
		//packet.addString(username);
		//packet.addString(password);

	}
}