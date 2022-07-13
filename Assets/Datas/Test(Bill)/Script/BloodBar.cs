using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BloodBar : MonoBehaviour
{
    [Header("血量")]
    private float maxBlood = 100;

    private float currentBlood = 100;

    public Image bloodBar;

    PlayerController Player;
    // Start is called before the first frame update
    void Start()
    {
        bloodBar = GetComponent<Image>();
        Player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentBlood = Player.Health;
        bloodBar.fillAmount = currentBlood / maxBlood;
    }
}
