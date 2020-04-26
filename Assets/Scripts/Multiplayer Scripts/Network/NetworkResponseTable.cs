using UnityEngine;

using System;
using System.Collections.Generic;

public class NetworkResponseTable {

	public static Dictionary<short, Type> responseTable { get; set; }
	
	public static void init() {
		responseTable = new Dictionary<short, Type>();
		add(Constants.SMSG_AUTH, "ResponseLogin");//201
		add(Constants.SMSG_PLAYERS, "ResponsePlayers");//203
		add(Constants.SMSG_TEST, "ResponseTest");//204
		add(Constants.SMSG_REG, "ResponseRegister");//205
		add(Constants.SMSG_ADDM, "ResponseAddMoney");//206
		add(Constants.SMSG_LVUP, "ResponseLvUp");//207
		add(Constants.SMSG_MATCH_PLAYER, "ResponseMatchPlayer");//208
	}
	
	public static void add(short response_id, string name) {
		responseTable.Add(response_id, Type.GetType(name));
	}
	
	public static NetworkResponse get(short response_id) {
		Debug.Log("requestTable.get()"+ response_id   + "NetworkResponse.cs line23");
		init ();
		NetworkResponse response = null;
		Debug.Log("response_id is :"+response_id);

		if (responseTable.ContainsKey(response_id)) {
			
			response = (NetworkResponse) Activator.CreateInstance(responseTable[response_id]);
			response.response_id = response_id;
			//Debug.Log(response == null? "response is  null!!!!" : "response is not null");
		} else {
			Debug.Log("Response [" + response_id + "] Not Found");
		}
		

		return response;
	}
}
