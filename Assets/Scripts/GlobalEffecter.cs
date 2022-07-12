using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEffecter : MonoBehaviour
{
    public EffectObjectSO[] effects;
    private Dictionary<string, EffectObjectSO> effect_dicts = new Dictionary<string, EffectObjectSO>();
    private void Start()
    {
        for (int i=0; i <effects.Length; i++) {
            effect_dicts.Add(effects[i].gcName , effects[i]);

            GameObject _obj = GameObject.Instantiate(effects[i].prefab);
            GCManager.RegisterObject(effects[i].gcName ,_obj);
        }
    }

    private void OnDestroy()
    {
        
    }


    public void DamageEffect(Vector3 _pos , int _damge) {
        string key = "Hurt";
        GCAutoRecall obj = GCManager.Instantiate<GCAutoRecall>(key);
        EffectObjectSO so = effect_dicts[key];
        obj.transform.position = _pos;
        obj.SetRecall("Hurt", so.duration);

    }


}
