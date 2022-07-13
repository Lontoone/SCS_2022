using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class JumpTextControl : MonoBehaviour
{
    public TextMeshProUGUI text;
    private Camera mainCamera;

    private void Start(){
mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if(mainCamera!=null)
            transform.LookAt(mainCamera.transform.position, Vector3.right);
        else            
            mainCamera = Camera.main;
    }
    public void SetText(Vector3 pos ,string _text) {
        text.text = _text;

        gameObject.transform.position = pos;

        Vector3 _endPos = gameObject.transform.position;
        _endPos.y += 5; 
        transform.DOMove(_endPos , 3);

    }
}
