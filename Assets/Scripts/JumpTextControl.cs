using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class JumpTextControl : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void FixedUpdate()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.right);
    }
    public void SetText(Vector3 pos ,string _text) {
        text.text = _text;

        gameObject.transform.position = pos;

        Vector3 _endPos = gameObject.transform.position;
        _endPos.y += 5; 
        transform.DOMove(_endPos , 3);

    }
}
