using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMove : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 _move = new Vector3(xMove,0,zMove);
        transform.position += _move * 5 * Time.deltaTime;
    }
}
