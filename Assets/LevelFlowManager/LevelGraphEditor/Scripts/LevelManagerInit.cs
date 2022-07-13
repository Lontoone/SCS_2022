using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerInit : MonoBehaviour
{
    public static LevelManagerInit instance;

    public GameObject[] dontDestoryObjs;
    public LevelMapSO levelData;

    void Start()
    {
        for (int i= 0; i<dontDestoryObjs.Length; i++) {
            DontDestroyOnLoad(dontDestoryObjs[i]);
        }
        DontDestroyOnLoad(gameObject);
        LevelFlowManager.flowData = levelData;
        LevelFlowManager.LoadFromStart("StartPoint");

        if (instance == null) {
            instance = this;
        }
    }


    public void ClearAll() {
        //TODO: ....
        for (int i =0; i<dontDestoryObjs.Length; i++) {
            Destroy(dontDestoryObjs[i]);
        }
        instance = null;
        Destroy(gameObject);
    }

}
