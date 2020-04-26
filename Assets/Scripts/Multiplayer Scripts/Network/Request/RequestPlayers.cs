using UnityEngine;
using System;

public class RequestPlayers : NetworkRequest {
		
	public RequestPlayers() {
		request_id = Constants.CMSG_PLAYERS;
	}
	
	public void send() {
		
		packet = new GamePacket(request_id);
	}

}