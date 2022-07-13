using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGUI : MonoBehaviour
{
    HitableObject player;
    public GameObject MenuCanvas;
    // Start is called before the first frame update
    void Start()
    {
        player.Die_event += ShowMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowMenu()
    {
        MenuCanvas.SetActive(true);
    }

    void OnDestroy()
    {
        player.Die_event -= ShowMenu;
    }
}
