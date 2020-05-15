using UnityEngine;

using System;

public class ResponseSelectMusicEventArgs : ExtendedEventArgs {
		
	public short status { get; set; }
	public int user_id { get; set; }
	public string username { get; set; }
	public int money { get; set; }
	public short level { get; set; }
	public string last_logout { get; set; }
	public int damage {get; set;}
	public int selectedMusicIndex {get; set;}
	
	public ResponseSelectMusicEventArgs() {
		event_id = Constants.SMSG_SELECTMUSIC;
	}
}

public class ResponseSelectMusic : NetworkResponse {
	
	private short status;
	private int user_id;
	private string username;
	private int money;
	private short level;
	private string last_logout;
	private int damage;
	private int selectedMusicIndex;

	public ResponseSelectMusic() {
		//Debug.Log("called ResponseAddMoney constructor");
	}
	
	public override void parse() {
		status = DataReader.ReadShort(dataStream);
		if (status == 0) {
			 user_id = DataReader.ReadInt(dataStream);
			 username = DataReader.ReadString(dataStream);
			// money = DataReader.ReadInt(dataStream);
			// level = DataReader.ReadShort (dataStream);
			// last_logout = DataReader.ReadString(dataStream);
			selectedMusicIndex = DataReader.ReadInt(dataStream);
		}
	}
	
	public override ExtendedEventArgs process() {
		ResponseSelectMusicEventArgs args = null;
		if (status == 0) {
			args = new ResponseSelectMusicEventArgs();
			args.status = status;
			 args.user_id = user_id;
			 args.username = username;
			// args.money = money;
			// args.level = level;
			// args.last_logout = last_logout;
			args.selectedMusicIndex = selectedMusicIndex;
		}

		return args;
	}
}