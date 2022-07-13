using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eatable : MonoBehaviour
{
    public float heal;

    private HitableObject hitable;

    private void Start()
    {
        hitable = gameObject.GetComponent<HitableObject>();
        hitable.Die_event += HealPlayer;
    }
    private void OnDestroy()
    {
        hitable.Die_event -= HealPlayer;
    }

    private void HealPlayer() {
        HitableObject.Hit_event_c(FindObjectOfType<PlayerControl>().gameObject , -heal);
    }
}
