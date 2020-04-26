using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalCounter : MonoBehaviour {

	public static int numElephants;
	public static int numVultures;
	public static int numMongooses;

	Text text;
	
	void Awake () {
		text = GetComponent<Text>();
		numElephants = 0;
		numVultures = 0;
		numMongooses = 0;
	}
	
	void Update () {
			text.text = "Elephants: " + numElephants + "\nVultures: " + numVultures + "\nMongooses: " + numMongooses;
	}

}
