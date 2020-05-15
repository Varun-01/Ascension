using System.Collections;
using UnityEngine;

public class FighterCamera : MonoBehaviour
{

    public float margin = 2f; // space between screen border and nearest fighter

    public float z0 = 2; // coord z of the fighters plane
    private float zCam; // camera distance to the fighters plane
    private float wScene; // scene width
    private Transform f1; // fighter1 transform
    private Transform f2; // fighter2 transform
    private float xL; // left screen X coordinate
    private float xR; // right screen X coordinate
    public string fighter1 = "Player1";
    public string fighter2 = "Player2";

    void calcScreen(Transform p1, Transform p2)
    {
        // Calculates the xL and xR screen coordinates 
        if (p1.position.x < p2.position.x)
        {
            xL = p1.position.x - margin;
            xR = p2.position.x + margin;
        }
        else
        {
            xL = p2.position.x - margin;
            xR = p1.position.x + margin;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // find references to the fighters
        f1 = GameObject.FindWithTag(fighter1).transform;
        f2 = GameObject.FindWithTag(fighter2).transform;
        
        // initializes scene size and camera distance
        calcScreen(f1, f2);
        wScene = xR - xL;
        zCam = transform.position.z - z0;
    }

    // Update is called once per frame
    void Update()
    {
        f1 = GameObject.FindWithTag(fighter1).transform;
        f2 = GameObject.FindWithTag(fighter2).transform;
        calcScreen(f1, f2);
        float width = xR - xL;
        if (width > wScene)
        { // if fighters too far adjust camera distance
            Vector3 newpos = transform.position;
            newpos.z = zCam * width / wScene + z0;
            transform.position = newpos;
            // transform.position.z = zCam * width / wScene + z0;
        }
        // centers the camera
        Vector3 newpos2 = transform.position;
        newpos2.x = (xR + xL) / 2;
        transform.position = newpos2;
       
    }
}

