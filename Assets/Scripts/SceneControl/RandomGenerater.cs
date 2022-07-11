using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerater : MonoBehaviour
{
    public GameObject prefab;
    public Bounds bound;
    public int minCount, maxCount;
    public float gap = 0.7f;

    private void Start()
    {
        DoRandomGenerate();
    }

    public void DoRandomGenerate()
    {
        int _randomCount = Random.Range(minCount, maxCount + 1);

        for (int i = 0; i < _randomCount; i++)
        {
            Vector3 pos = bound.min;
            pos.x += (gap*i + Random.Range(gap,gap*2)) % bound.size.x;
            pos.z += (gap*i + Random.Range(gap, gap * 2)) % bound.size.z;
            GameObject _newEnemy = Instantiate(prefab, pos + transform.position, Quaternion.identity);
        }
        int _rand = Random.Range(0, 100);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(bound.center+transform.position , bound.size);
    }
}
