using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class TestSceneScript : MonoBehaviour {
		public GameObject terrainNew;
		private Rect windowRect;
	// Use this for initialization
	void Start () {
		GameObject terrainOld = GameObject.Find ("MaasaiMara");
		Vector3 position = terrainOld.transform.position;
		Quaternion rotation = terrainOld.transform.rotation;
		Destroy(terrainOld);
		GameObject newTerrain =(GameObject) Instantiate (terrainNew , position , rotation);
		newTerrain.name = "new terrain";
		newTerrain.layer = 9;
		newTerrain.GetComponent<Terrain>().enabled = true;
	}

public void loadMultiplayerScene(){
	Debug.Log("load scence clicked");
	SceneManager.LoadScene("Multiplayer");
}

}
