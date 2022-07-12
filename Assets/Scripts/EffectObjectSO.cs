using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EffectObj")]
public class EffectObjectSO : ScriptableObject
{
    public GameObject prefab;
    public float duration;
    public string gcName;
}
