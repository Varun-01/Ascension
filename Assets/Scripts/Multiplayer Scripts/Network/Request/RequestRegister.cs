using UnityEngine;

using System;

public class RequestRegister : NetworkRequest {

	public RequestRegister() {
		request_id = Constants.CMSG_REG;
	}
	
	public void send(string username, string password) {
	    packet = new GamePacket(request_id);
		packet.addString(Constants.CLIENT_VERSION);
		packet.addString(username);
		packet.addString(password);
	}
}