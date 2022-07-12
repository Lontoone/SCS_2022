using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("移動速度")]
    int MoveSpeed = 20;

    [Header("攻擊範圍")]
    int MaxDist = 3;

    int MinDist = 1;

    //[Header("動畫")]
    //public Animator m_Animator;

    public GameObject[] PlayerObj;

    public GameObject Medic;

    float X;
    //public AudioSource As;
    void Start()
    {
        PlayerObj = GameObject.FindGameObjectsWithTag("Player");
        PlayerObj[0].transform.position = PlayerObj[0].transform.position;
        // m_Animator = GetComponent<Animator>();
    }


    void Update()
    {

        transform.LookAt(PlayerObj[0].transform);



        if (Vector3.Distance(transform.position, PlayerObj[0].transform.position) >= MinDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, PlayerObj[0].transform.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
                MoveSpeed = 0;
                // m_Animator.SetBool("isAttack", true);
            }
            else
            {
                MoveSpeed = 10;
                // m_Animator.SetBool("isAttack", false);
            }



        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            X = Random.Range(0, 2);
            Destroy(gameObject);
            Debug.Log(X);
            if (X == 1)
            {
                Instantiate(Medic, transform.position, transform.rotation);
            }
            
        }
    }
}