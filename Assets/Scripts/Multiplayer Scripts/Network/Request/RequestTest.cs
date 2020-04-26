using UnityEngine;
using System;

public class RequestTest : NetworkRequest {

		public RequestTest() {
				request_id = Constants.CMSG_TEST;
		}

		public void send(string arithmeticOperator, int testNum) {
				packet = new GamePacket(request_id);
				packet.addString (arithmeticOperator);
				packet.addInt32 (testNum);
		}

}
