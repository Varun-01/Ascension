using UnityEngine;

using System;

public class RequestSelectMusic : NetworkRequest {

	public RequestSelectMusic() {
		request_id = Constants.CMSG_SELECTMUSIC;
	}
	
	public void send(int selectedMusicIndex) {
		Debug.Log ("called requestSelectMusic.send() function: "+ request_id);
	    packet = new GamePacket(request_id);
		//packet.addString(Constants.CLIENT_VERSION);
		//packet.addString(attackName);
		packet.addInt32(selectedMusicIndex);
		//packet.addString(username);
		//packet.addString(password);

	}
}