using UnityEngine;
using System.Collections;

public class MouseCollision : MonoBehaviour {
		
	public Transform animal;
	private int layerMask = 1 << 9;

	void Start () {
	
	}
	
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit, Mathf.Infinity, layerMask)){
				if (hit.collider != null) {
					Vector3 position = hit.collider.transform.position;
					Instantiate (animal, hit.point, Quaternion.identity);			
					AnimalCounter.numElephants += 1;
					}
			}
		}
	
	}

}