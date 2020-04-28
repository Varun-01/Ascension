using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTension : MonoBehaviour
{

    public int minTension = 0;
    public int currentTension;

    //add in tension bar object
    public TensionBar tensionBar;

    // Start is called before the first frame update
    void Start()
    {
        currentTension = minTension;
        //adjust tensionbar
        tensionBar.SetMinTension(minTension);
    }

    // Update is called once per frame

    //take 10 damage on space bar press
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseTension(10);
        }
    }

    void IncreaseTension(int tension)
    {
        currentTension += tension;
        //adjust tensionbar
        tensionBar.SetTension(currentTension);

    }
}
