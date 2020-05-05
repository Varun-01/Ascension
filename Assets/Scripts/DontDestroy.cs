using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy playerInstance;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DoNotDestroy");

        //if (objs.Length > 1)
        //{
          //  Destroy(this.gameObject);
        //}

        DontDestroyOnLoad(this.gameObject);
        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }
}
