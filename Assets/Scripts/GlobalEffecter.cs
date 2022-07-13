using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GlobalEffecter : MonoBehaviour
{
    public EffectObjectSO[] effects;
    private Dictionary<string, EffectObjectSO> effect_dicts = new Dictionary<string, EffectObjectSO>();


    private void Start()
    {

        HitableObject.HitEffect_event += DamageEffect;
        HitableObject.HitEffect_event += PopUpText;

    }

    private void OnDestroy()
    {
        HitableObject.HitEffect_event -= DamageEffect;
        HitableObject.HitEffect_event -= PopUpText;
    }

    private void Init() {

        effect_dicts.Clear();
        for (int i=0; i <effects.Length; i++) {
            effect_dicts.Add(effects[i].gcName , effects[i]);

            GameObject _obj = GameObject.Instantiate(effects[i].prefab);
            GCManager.RegisterObject(effects[i].gcName ,_obj);

        }
    }

    private void OnLevelWasLoaded(int level)
    {
        GCManager.Clear();
        Init();

    }

    public void DamageEffect(Vector3 _pos , float _damge) {
        string key = "Attack";
        GCAutoRecall obj = GCManager.Instantiate<GCAutoRecall>(key);
        EffectObjectSO so = effect_dicts[key];
        obj.transform.position = _pos;
        obj.SetRecall(key, so.duration);
        AudioController.Instance.SpawnAudio(0);
        ShakeCamera();
    }

    public void PopUpText(Vector3 _pos , float _damage) {
        string key = "PopUpText";
        GCAutoRecall obj = GCManager.Instantiate<GCAutoRecall>(key);
        EffectObjectSO so = effect_dicts[key];        
        obj.transform.position = _pos;
        obj.SetRecall(key, so.duration);

        obj.GetComponent<JumpTextControl>().SetText(_pos,_damage.ToString());

    }

    public void ShakeCamera() {
        Camera.main.DOShakePosition(0.05f);
    }


}
