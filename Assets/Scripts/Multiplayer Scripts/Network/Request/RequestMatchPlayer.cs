using UnityEngine;

using System;

public class RequestMatchPlayer : NetworkRequest {

	public RequestMatchPlayer() {
		request_id = Constants.CMSG_MATCH_PLAYER;
	}
	
	public void send() {
	    packet = new GamePacket(request_id);
		//packet.addString(Constants.CLIENT_VERSION);
		//packet.addString(username);
		//packet.addString(password);
	}
}