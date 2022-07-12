using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTriggerConnectPoint : ConnectPoint
{
    private bool leaveLock = false;
    public void OnEnable()
    {
        LevelFlowManager.OnConnectPointEntered += MovePlayerHere;
        Debug.Log("Invoke range awake");
    }
    public void OnDisable()
    {
        LevelFlowManager.OnConnectPointEntered -= MovePlayerHere;
    }
 
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if (!leaveLock && collision.gameObject.tag == "Player")
        {
            LevelFlowManager.LoadNextScene(portSetId);
        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            leaveLock = false;
        }
    }

    private void MovePlayerHere(string _enterPoint)
    {
        //Debug.Log("Invoke called " + _enterPoint + " mine " + portSetId);
        if (_enterPoint == portSetId)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
            leaveLock = true;
        }
    }
}
