using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyMenus: MonoBehaviour
{
    private static DoNotDestroyMenus mainCamera;
    private static DoNotDestroyMenus selectionManager;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DoNotDestroy");

        //if (objs.Length > 1)
        //{
        //  Destroy(this.gameObject);
        //}

        DontDestroyOnLoad(this.gameObject);
        
        if (this.gameObject.name == "Main Camera" && mainCamera == null)
        {
            mainCamera = this;
        } else if (this.gameObject.name == "SelectionManager" && selectionManager == null)
        {
            selectionManager = this;
        }else
        {
            DestroyObject(gameObject);
        }
    }
}
