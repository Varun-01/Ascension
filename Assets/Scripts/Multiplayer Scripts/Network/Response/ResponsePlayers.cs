using UnityEngine;
using System;

public class ResponsePlayersEventArgs : ExtendedEventArgs {
		
	public int numActivePlayers { get; set; }

	public ResponsePlayersEventArgs() {
		event_id = Constants.SMSG_PLAYERS;
	}

}

public class ResponsePlayers : NetworkResponse {

	private int numActivePlayers;

	public ResponsePlayers() {
	}

	public override void parse() {
		numActivePlayers = DataReader.ReadInt(dataStream);
	}

	public override ExtendedEventArgs process() {
		ResponsePlayersEventArgs args = null;
		args = new ResponsePlayersEventArgs();
		args.numActivePlayers = numActivePlayers;
		return args;
	}

}