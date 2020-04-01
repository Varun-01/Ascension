using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    private Transform f1;
    private Transform f2;
    public string fighter1 = "Player1";
    public string fighter2 = "Player2";
    float fp1;
    float fp2;
    float timeToGo;
    // Update is called once per frame
    void Start()
    {
        f1 = GameObject.FindWithTag(fighter1).transform;
        f2 = GameObject.FindWithTag(fighter2).transform;
        timeToGo = Time.fixedTime + 60.0f;
    }
    void FixedUpdate()
    {
        if (f1.position.x>f2.position.x)
        {
            //player 1
            Vector3 temp = transform.rotation.eulerAngles;
            temp.y = -95.0f;
            f1.transform.rotation = Quaternion.Euler(temp);
            //player 2
            Vector3 temp2 = transform.rotation.eulerAngles;
            temp2.y = 95.0f;
            f2.transform.rotation = Quaternion.Euler(temp2);
        }
        else 
        {
            //player 1
            Vector3 temp = transform.rotation.eulerAngles;
            temp.y = 95.0f;
            f1.transform.rotation = Quaternion.Euler(temp);
            //player 2
            Vector3 temp2 = transform.rotation.eulerAngles;
            temp2.y = -95.0f;
            f2.transform.rotation = Quaternion.Euler(temp2);
        }
        timeToGo = Time.fixedTime + 60.0f;

    }
    
}
