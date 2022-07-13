using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public LayerMask attackLayer;
    public Bounds attackBound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] _attacked = Physics.OverlapBox(attackBound.center + transform.position, attackBound.extents, Quaternion.identity, attackLayer);
        for (int i = 0; i < _attacked.Length; i++)
        {
            HitableObject.Hit_event_c(_attacked[i].gameObject, 30);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackBound.center + transform.position, attackBound.size);
    }
}
