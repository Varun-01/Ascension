using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public float p1x;
    public float p1y;
    public float p1z;
    public float p2x;
    public float p2y;
    public float p2z;

    public string player1;
    public string player2;
    // Start is called before the first frame update
    void Start()
    {
        GameObject character = GameObject.Find("SelectionManager");
        Selection_Manager mChar = character.GetComponent<Selection_Manager>();
        player1 = mChar.getCharacter1();
        player2 = mChar.getCharacter2();
        if (player1 == player2)
        {
            player2 = mChar.getCharacter2()+" Alt";
        }
        
        Transform myItem = (Instantiate(Resources.Load(player1), new Vector3(p1x, p1y, p1z), Quaternion.Euler(0, -95, 0)) as GameObject).transform;
        Transform myItem1 = (Instantiate(Resources.Load(player2), new Vector3(p2x, p2y, p2z), Quaternion.Euler(0, 95, 0)) as GameObject).transform;
        
        myItem.tag = "Player1";
        myItem1.tag = "Player2";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
