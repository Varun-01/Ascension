using UnityEngine;

using System;

public class ResponseMatchPlayerEventArgs : ExtendedEventArgs {
		
	public short status { get; set; }
	public int user_id { get; set; }

	public int opponent_id{get; set;}
	public string username { get; set; }
	public int money { get; set; }
	public short level { get; set; }
	public string last_logout { get; set; }
	
	public ResponseMatchPlayerEventArgs() {
		event_id = Constants.SMSG_MATCH_PLAYER;
	}
}

public class ResponseMatchPlayer : NetworkResponse {
	
	private short status;
	private int user_id, opponent_id;
	private string username;
	private int money;
	private short level;
	private string last_logout;

	public ResponseMatchPlayer() {
	}
	
	public override void parse() {
		status = DataReader.ReadShort(dataStream);
		if (status == 0) {
			user_id = DataReader.ReadInt(dataStream);
			username = DataReader.ReadString(dataStream);
			money = DataReader.ReadInt(dataStream);
			level = DataReader.ReadShort (dataStream);
			opponent_id = DataReader.ReadInt(dataStream);
			last_logout = DataReader.ReadString(dataStream);
			Debug.Log("*************************Opponent_id in response(Parse()): " + opponent_id);
			
		}
	}
	
	public override ExtendedEventArgs process() {
		ResponseMatchPlayerEventArgs args = null;
		

		if (status == 0) {
			args = new ResponseMatchPlayerEventArgs();
			args.status = status;
			args.user_id = user_id;
			args.username = username;
			args.money = money;
			args.level = level;
			args.last_logout = last_logout;
			args.opponent_id = opponent_id;
		}

		return args;
	}
}