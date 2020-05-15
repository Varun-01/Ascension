using UnityEngine;

using System;
using System.Collections.Generic;

public class NetworkRequestTable {

	public static Dictionary<short, Type> requestTable { get; set; }
	
	public static void init() {
		requestTable = new Dictionary<short, Type>();
		add(Constants.CMSG_AUTH, "RequestLogin");//101
		add(Constants.CMSG_HEARTBEAT, "RequestHeartbeat");//102
		add(Constants.CMSG_PLAYERS, "RequestPlayers");
		add(Constants.CMSG_TEST, "RequestTest");
		add(Constants.CMSG_REG, "RequestRegister");//105
		add(Constants.CMSG_ATT, "RequestAttack");//106
		add(Constants.CMSG_MOVE, "RequestMove");//107
		add(Constants.CMSG_MATCH_PLAYER, "RequestMatchPlayer");//108
		add(Constants.CMSG_SELECTIONS, "RequestSelections");//109
		add(Constants.CMSG_SELECTSTAGE,"RequestSelectStage");//210
		add(Constants.CMSG_SELECTMUSIC, "RequestSelectMusic");//211
	}
	
	public static void add(short request_id, string name) {
		requestTable.Add(request_id, Type.GetType(name));
	}
	
	public static NetworkRequest get(short request_id) {
		NetworkRequest request = null;
		
		if (requestTable.ContainsKey(request_id)) {
			request = (NetworkRequest) Activator.CreateInstance(requestTable[request_id]);
			request.request_id = request_id;
		} else {
			Debug.Log("Request [" + request_id + "] Not Found");
		}
		
		return request;
	}
}
