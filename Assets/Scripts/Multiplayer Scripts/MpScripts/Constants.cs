public class Constants {
	
	// Constants
	public static readonly string CLIENT_VERSION = "1.00";
	public static readonly string REMOTE_HOST = "192.168.0.19";

	//public static readonly string REMOTE_HOST = "71.198.184.68";
	public static readonly int REMOTE_PORT = 9252;
	
	// Request (1xx) + Response (2xx)
	public static readonly short CMSG_AUTH = 101;
	public static readonly short SMSG_AUTH = 201;
    public static readonly short CMSG_HEARTBEAT = 102;
    public static readonly short SMSG_HEARTBEAT = 202;
	public static readonly short CMSG_PLAYERS = 103;
	public static readonly short SMSG_PLAYERS = 203;
	public static readonly short CMSG_TEST = 104;
	public static readonly short SMSG_TEST = 204;
	public static readonly short CMSG_REG = 105;
	public static readonly short SMSG_REG = 205;
	public static readonly short CMSG_ATT = 106;
	public static readonly short SMSG_ATT = 206;
	public static readonly short CMSG_MOVE = 107;
	public static readonly short SMSG_MOVE = 207;
	public static readonly short CMSG_MATCH_PLAYER = 108;
	public static readonly short SMSG_MATCH_PLAYER = 208;
	
	// Other
	public static readonly string IMAGE_RESOURCES_PATH = "Images/";
	public static readonly string PREFAB_RESOURCES_PATH = "Prefabs/";
	public static readonly string TEXTURE_RESOURCES_PATH = "Textures/";
	
	// GUI Window IDs
	public enum GUI_ID {
		Login,
		Register,
		AddMoney
	};

	public static int USER_ID = -1;
}