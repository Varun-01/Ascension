using UnityEngine;

using System;

public class RequestSelectStage : NetworkRequest {

	public RequestSelectStage() {
		request_id = Constants.CMSG_SELECTSTAGE;
	}
	
	public void send(int selectedStageIndex) {
		Debug.Log ("called requestSelection.send() function: "+ request_id);
	    packet = new GamePacket(request_id);
		//packet.addString(Constants.CLIENT_VERSION);
		//packet.addString(attackName);
		packet.addInt32(selectedStageIndex);
		//packet.addString(username);
		//packet.addString(password);

	}
}