using UnityEngine;

using System;

public class RequestLvUp : NetworkRequest {

	public RequestLvUp() {
		request_id = Constants.CMSG_LVUP;
	}
	
	public void send() {
		Debug.Log ("called requestAddMoney.send() function: "+ request_id);
	    packet = new GamePacket(request_id);
		packet.addString(Constants.CLIENT_VERSION);
		//packet.addInt32(moneyToAdd);
		//packet.addString(username);
		//packet.addString(password);

	}
}