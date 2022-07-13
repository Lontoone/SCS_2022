using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakToPiece : MonoBehaviour
{
    private HitableObject hitable;
    public GameObject prefab;

    private void Start()
    {
        hitable = gameObject.GetComponent<HitableObject>();
        hitable.Die_event += OnDie;

    }

    private void OnDestroy()
    {
        hitable.Die_event -= OnDie;
    }
    [ContextMenu("test")]
    public void Test() {
        HitableObject.Hit_event_c(gameObject, 50);
    }

    private void OnDie() {
        Destroy(gameObject);
        GameObject pieces = GameObject.Instantiate(prefab);
        prefab.transform.position = transform.position;

        foreach (Transform child in pieces.transform) {
            Rigidbody rd = child.gameObject.GetComponent<Rigidbody>();

            Vector3 dir = child.transform.position - transform.position;
            rd.AddForce(dir.normalized * 15);

        }
        Destroy(pieces , 15);
    }
}
