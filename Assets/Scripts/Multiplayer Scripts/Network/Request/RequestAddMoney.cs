using UnityEngine;

using System;

public class RequestAddMoney : NetworkRequest {

	public RequestAddMoney() {
		request_id = Constants.CMSG_ADDM;
	}
	
	public void send(int moneyToAdd) {
		Debug.Log ("called requestAddMoney.send() function: "+ request_id);
	    packet = new GamePacket(request_id);
		packet.addString(Constants.CLIENT_VERSION);
		packet.addInt32(moneyToAdd);
		//packet.addString(username);
		//packet.addString(password);

	}
}