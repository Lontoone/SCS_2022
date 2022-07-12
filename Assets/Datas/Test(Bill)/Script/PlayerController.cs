using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    public Bounds attackBound;
    private HitableObject hitable;
    public static event Action<Vector3,int> OnHurt;
    [Header("移動")]
    public float moveSpeed = 3f;

    public float horizontal;

    public float Vertical;

    Rigidbody RB;

    [Header("閃避")]
    public float DashSpeed;

    public bool isDashForward;

    public bool isDashRight;

    public bool canDash;

    public GameObject dashEffect;

    public float Health;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        hitable = gameObject.GetComponent<HitableObject>();
        hitable.gotHit_event += GetHurt;
        canDash = true;
    }
    private void OnDestroy()
    {
        hitable.gotHit_event -= GetHurt;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        Vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, Vertical).normalized;

        RB.velocity = new Vector3(horizontal * moveSpeed, 0, Vertical * moveSpeed);

        if(Input.GetKeyDown(KeyCode.LeftShift) && Vertical > 0 && canDash)
        {
            isDashForward = true;
            DashSpeed = 100;

        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Vertical < 0 && canDash)
        {
            isDashForward = true;
            DashSpeed = -100;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && horizontal > 0 && canDash)
        {
            isDashRight = true;
            DashSpeed = 100;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && horizontal < 0 && canDash)
        {
            isDashRight = true;
            DashSpeed = -100;
        }

        if(Input.GetKey(KeyCode.C))
        {
            moveSpeed = 10;
        }
        else if(Input.GetKeyUp(KeyCode.C))
        {
            moveSpeed = 3;
        }

        if (Input.GetMouseButtonDown(0)) {
            Collider[] _attacked = Physics.OverlapBox(attackBound.center+transform.position, attackBound.extents);
            for (int i =0; i< _attacked.Length; i++) {
                HitableObject.Hit_event_c(_attacked[i].gameObject , 30);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDashForward)
        {
            DashForward();
        }
        if(isDashRight)
        {
            DashRight();
        }
    }

    private void DashForward()
    {
        RB.AddForce(transform.forward * DashSpeed, ForceMode.Impulse);
        isDashForward = false;
        canDash = false;

        GameObject effect = Instantiate(dashEffect, Camera.main.transform.position, dashEffect.transform.rotation);

        effect.transform.parent = Camera.main.transform;
        effect.transform.LookAt(transform);

        Invoke("CanDash", 2f);
    }

    private void DashRight()
    {
        RB.AddForce(transform.right * DashSpeed, ForceMode.Impulse);
        isDashRight = false;
        canDash = false;

        GameObject effect = Instantiate(dashEffect, Camera.main.transform.position, dashEffect.transform.rotation);

        effect.transform.parent = Camera.main.transform;
        effect.transform.LookAt(transform);
    }

    void CanDash()
    {
        canDash = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Collider>().tag == "Enemy")
        {
            GetHurt();
        }
        if (other.GetComponent<Collider>().tag == "Medic")
        {
            GetHealth();
            Destroy(other.gameObject);
        }
    }

    void GetHurt()
    {
        int damage = 15;
        Health = Health - 15;
        //Debug.Log("Hurt");
        OnHurt?.Invoke(transform.position , damage);
    }

    void GetHealth()
    {
        Health = Health + 15;
        //Debug.Log("Health");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackBound.center +transform.position , attackBound.size);
    }
}
