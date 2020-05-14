using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroyMenus: MonoBehaviour
{
    //private static DoNotDestroyMenus mainCamera;
    //private static DoNotDestroyMenus selectionManager;

    private static GameObject mainCamera;
    private static GameObject selectionManager;
    private GameObject[] objs;
    private GameObject[] objs1;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DoNotDestroyMusic");
        GameObject[] objs1 = GameObject.FindGameObjectsWithTag("DoNotDestroySelectionManager");

        if (objs.Length > 1 || objs1.Length > 1)
        {
          Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        /*
        if (this.gameObject.name == "Main Camera" && mainCamera == null)
        {
            mainCamera = this.gameObject;

        } else if (this.gameObject.name == "SelectionManager" && selectionManager == null)
        {
            selectionManager = this.gameObject;
        }else
        {
            DestroyObject(gameObject);
        }*/

        //Scene currentScene = SceneManager.GetActiveScene();
    }
    void Update() {

        if (Application.loadedLevelName == "MusicSelect")
        {
            Destroy(objs[0]);
        }
    }
}
