using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPBar : MonoBehaviour
{
    HitableObject hitable;
    public TextMeshProUGUI hpText;

    private void Start()
    {
        hitable = gameObject.GetComponent<HitableObject>();
        hitable.gotHit_event += UpdatHPText;
    }
    private void OnDestroy()
    {
        hitable.gotHit_event -= UpdatHPText;
    }

    private void UpdatHPText() {
        hpText.text = hitable.HP.ToString();
    }

}
