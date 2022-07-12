using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    
    private int currentLvCount = 0;
    private BlackScreenEffect _bse;
    public BlackScreenEffect blackScreenFX
    {
        get
        {
            if (_bse == null)
            {
                _bse = Camera.main.GetComponent<BlackScreenEffect>();
                return _bse;
            }
            else return _bse;
        }
        set {
            _bse = value;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        currentLvCount++;
        //float _newMaskMag = Mathf.Clamp01(blackScreenFX.maskRangeSize - 0.1f*currentLvCount);
        float _newMaskMag = Mathf.Clamp01(0.9f - 0.05f * currentLvCount);
        blackScreenFX.SetMaskMag(_newMaskMag);
    }

}
