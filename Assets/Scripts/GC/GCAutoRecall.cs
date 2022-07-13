using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCAutoRecall : MonoBehaviour
{
    private void OnEnable()
    {
        
    }
    private IEnumerator SetRecallCoro(string _key, float _time) {
        yield return new WaitForSeconds(_time);
        GCManager.Destory(_key, gameObject);
    }

    public void SetRecall(string _key ,float _time) {
        StartCoroutine(SetRecallCoro(_key , _time));
    }
}
