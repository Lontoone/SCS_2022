using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private HitableObject hitable;
    public Bounds attackRange;
    [Header("移動速度")]
    public int MoveSpeed = 3;

    [Header("攻擊範圍")]
    int MaxDist = 3;

    int MinDist = 1;

    [Header("動畫")]
    public Animator m_Animator;

    public GameObject[] PlayerObj;

    public GameObject Medic;

    float X;

    public int Blood = 100;

    public LayerMask attackLayer;
    public Bounds attackBound;
    //public AudioSource As;
    void Start()
    {
        PlayerObj = GameObject.FindGameObjectsWithTag("Player");
        PlayerObj[0].transform.position = PlayerObj[0].transform.position;
        hitable = gameObject.GetComponent<HitableObject>();
        hitable.gotHit_event += GetHurt;
        hitable.Die_event += Die;
        m_Animator = GetComponent<Animator>();   
    }
    private void OnDestroy()
    {
        hitable.gotHit_event -= GetHurt;
        hitable.Die_event -= Die;
    }



    void Update()
    {

        transform.LookAt(PlayerObj[0].transform);

        SearchandAttack();
        Collider[] _attacked = Physics.OverlapBox(attackBound.center + transform.position, attackBound.extents, Quaternion.identity, attackLayer);
        for (int i = 0; i < _attacked.Length; i++)
        {
            HitableObject.Hit_event_c(_attacked[i].gameObject, 30);
        }

    }

    void SearchandAttack()
    {
        if (Vector3.Distance(transform.position, PlayerObj[0].transform.position) >= MinDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, PlayerObj[0].transform.position) <= MaxDist)
            {
                MoveSpeed = 0;
            }
            else
            {
                MoveSpeed = 10;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        HitableObject.Hit_event_c(other.gameObject , 20);
        /*
        if (other.GetComponent<Collider>().tag == "Player")
        {
            GetHurt();
        }*/
    }

    void GetHurt()
    {
        X = Random.Range(0, 2);
        //Destroy(gameObject);
        Debug.Log(X);
        if (X == 1)
        {
            Instantiate(Medic, transform.position, transform.rotation);
            //Blood = Blood - 50;
        }
    }

    void Die() {
        //desyoty...
        Destroy(gameObject,2f);
        m_Animator.SetBool("isDead", true);
        Debug.Log("EnemyDie");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackBound.center + transform.position, attackBound.size);
    }
}