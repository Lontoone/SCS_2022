using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerInit : MonoBehaviour
{
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
    }


    public void ClearAll() { 
        //TODO: ....
    }

}
