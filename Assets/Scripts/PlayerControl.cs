using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//玩家控制
public class PlayerControl : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 10;
    Animator animator;
    Rigidbody rigid;
    PhysicsControlListeners listeners;
    HitableObject hitable;

    public ActionController.mAction idle, walk, run, hurt, assemble;
    ActionController actionController;

    public static event Action eAssamble, eEndAssamble;

    int jump_count = 0; //跳躍次數 (for 2段跳)

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        listeners = gameObject.GetComponent<PhysicsControlListeners>();
        animator = gameObject.GetComponent<Animator>();
        hitable = gameObject.GetComponent<HitableObject>();
        actionController = gameObject.GetComponent<ActionController>();

        if (hitable != null)
        {
            hitable.Die_event += Die;
            hitable.gotHit_event += Hurt;
        }
        if (listeners != null)
            listeners.eOnTouchGround += ResetJumpCount;
    }
    private void OnDestroy()
    {
        if (hitable != null)
        {
            hitable.Die_event -= Die;
            hitable.gotHit_event -= Hurt;
        }
        if (listeners != null)
            listeners.eOnTouchGround -= ResetJumpCount;
    }
    private void Update()
    {
        float hx_input = Input.GetAxis("Horizontal");
        float vz_input = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(vz_input, 0, -hx_input) * speed;
        Debug.Log("player move " + move.magnitude);
        if (move.magnitude > 10f)
        {
            Debug.Log(rigid.velocity.magnitude + "run");
            actionController.AddAction(run);
        }
        else if (move.magnitude > 0.1f)
        {
            Debug.Log(rigid.velocity.magnitude + "walk");
            actionController.AddAction(walk);
        }
        else
        {
            actionController.AddAction(idle);
        }


    }

    private void FixedUpdate()
    {
        //召集史萊姆:
        if (Input.GetKey(KeyCode.R))
        {
            actionController.AddAction(assemble);
            //animator.Play("Assamble");
            if (eAssamble != null)
            {
                eAssamble();
            }
        }
        if (Input.GetKeyUp(KeyCode.R) && eEndAssamble != null)
        {
            eEndAssamble();
        }
    }

    public void Move()
    {
        float hx_input = Input.GetAxis("Horizontal");
        float vz_input = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(vz_input, 0, -hx_input) * speed;
        rigid.velocity = new Vector3(move.x, rigid.velocity.y, move.z);

        if (rigid.velocity.magnitude > 0.5f)
            transform.rotation = Quaternion.LookRotation(move);
    }
    public void Walk()
    {
        animator.Play("Walk");
    }
    public void Run()
    {
        animator.Play("Run");
    }

    public void Hurt()
    {
        if (hitable.isHitable)
        {
            //被擊退
            //playerAttack.input_s="Hurt";
            //animator.Play("Hurt");
            actionController.AddAction(hurt);
        }
    }
    void Die()
    {
        Debug.Log("玩家死亡");
        //GameoverManager.ShowGameResult(false);
    }

    void ResetJumpCount()
    {
        jump_count = 0;
        animator.Play("Idle");
    }
}
