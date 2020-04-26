using UnityEngine;
using System;

public class ResponseTestEventArgs : ExtendedEventArgs {

	public ResponseTestEventArgs() {
		event_id = Constants.SMSG_TEST;
	}

}

public class ResponseTest : NetworkResponse {

	public ResponseTest() {
	}

	public override void parse() {
	}

	public override ExtendedEventArgs process() {
		ResponseTestEventArgs args = null;
		args = new ResponseTestEventArgs();
		return args;
	}

}