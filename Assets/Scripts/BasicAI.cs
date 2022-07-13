using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//自動移動

public class BasicAI : MonoBehaviour
{

    Rigidbody rigid;
    public Vector2 speed = new Vector2(5, 0); //平面,飛行


    [HideInInspector]
    public bool reached_goal = false;//抵達?

    //EVENT
    public event Action walk_event;
    public event Action idle_event;
    public event Action jump_event;
    public event Action eTarget_find;
    public event Action eMoveGoal_reached;//到達目的地...


    public ColliderDetector enemy_detect_collider;//偵測攻擊對象的collider
    public ColliderDetector attack_collider;//攻擊collider範圍
    public GameObject attackingObj;
    public float damage = 5;


    Vector3 moveDir = new Vector3(); // 隨機移動方向

    Animator animator;
    ActionController actionController;

    public ActionController.mAction idle_act = new ActionController.mAction(
            null,
            "Idle",
            0,
            false,
            3,
            10
    );
    //walk_act, default_stop;
    public ActionController.mAction walk_act = new ActionController.mAction(
            null,
            "Move",
            1,
            false,
            3,
            10
    );
    public ActionController.mAction hurt_act = new ActionController.mAction();
    public ActionController.mAction attack;
    HitableObject hitable;

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        actionController = gameObject.GetComponent<ActionController>();
        hitable = gameObject.GetComponent<HitableObject>();

        if (enemy_detect_collider != null)
        {
            enemy_detect_collider.mOnTriggerStay += SetAttackingTarget;
            enemy_detect_collider.mOnTriggerStay += Attack;
            enemy_detect_collider.mOnTriggerLeave += LostAttackingTarget;
            //enemy_detect_collider.mOnTriggerEnter += Attack;
        }
        actionController.eActionQueueCleared += AddDefaultAction;

        hitable.gotHit_event += Hurt;
        hitable.Die_event += Die;

        //Start with Idle
        actionController.AddAction(idle_act);
    }
    private void OnDestroy()
    {
        actionController.eActionQueueCleared -= AddDefaultAction;
        if (enemy_detect_collider != null)
        {
            enemy_detect_collider.mOnTriggerStay -= SetAttackingTarget;
            enemy_detect_collider.mOnTriggerStay -= Attack;
            enemy_detect_collider.mOnTriggerLeave -= LostAttackingTarget;
            //enemy_detect_collider.mOnTriggerEnter -= Attack;
        }

        hitable.gotHit_event -= Hurt;
        hitable.Die_event -= Die;
    }

    //找到攻擊目標
    void SetAttackingTarget(GameObject target)
    {
        if (attackingObj == null)
        {
            attackingObj = target;
        }
    }
    //失去攻擊目標
    void LostAttackingTarget(GameObject target)
    {
        if (attackingObj == target)
        {
            attackingObj = null;
        }
    }

    //攻擊
    public void Attack(GameObject _enterObj)
    {
        if (actionController == null)
            return;
        actionController.AddAction(attack);
        //animator.Play("Attack");
        /*
        foreach (GameObject _hits in attack_collider.collidersInRange)
        {
            HitableObj.Hit_event_c(_hits, damage, gameObject);
        }*/
    }
    public virtual void Attack_anima_event(float damage_multiplier) //由Animator event呼叫
    {

        for (int i = 0; i < attack_collider.collidersInRange.Count; i++)
        {
            HitableObject.Hit_event_c(attack_collider.collidersInRange[i].gameObject, damage_multiplier * damage);
        }

    }


    public void Idle()
    {
        animator.Play("Idle");
    }
    public void Walk()
    {
        if (rigid == null) { return; }
        //隨機移動
        if (attackingObj == null)
        {
            //Vector3 move = dir * speed;
            //rigid.velocity = moveDir * speed;
            rigid.velocity = new Vector3(moveDir.x * speed.x, moveDir.y * speed.y, moveDir.z * speed.x);
        }

        //追逐目標
        else
        {
            Vector3 move = (attackingObj.transform.position - transform.position).normalized;
            rigid.velocity = new Vector3(move.x * speed.x, move.y * speed.y, move.z * speed.x);

        }

        //transform.rotation = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.LookRotation(rigid.velocity);
        animator.Play("Walk");
    }


    void Hurt()
    {
        actionController.AddAction(hurt_act);
    }
    void Die()
    {
        animator.Play("Die");

        Destroy(actionController);
        Destroy(gameObject,2);
    }


    //沒事做就先塞個default工作
    void AddDefaultAction()
    {
        int ran = UnityEngine.Random.Range(0, 100);
        Debug.Log("Add func" + ran);
        //IDLE
        if ((ran < 50 || walk_act.is_in_gap_time_lock) && attackingObj == null)
        {

            actionController.AddAction(idle_act);
        }
        else
        {
            FindRandomMoveGoal();

            actionController.AddAction(walk_act);
        }
    }

    //隨機設定移動目標
    public void FindRandomMoveGoal()
    {
        Debug.Log("FindRandomMoveGoal");
        Vector3 _rand_point = new Vector3(transform.position.x + UnityEngine.Random.Range(-20, 20),
                                        transform.position.y,
                                        transform.position.z + UnityEngine.Random.Range(-20, 20));
        moveDir = (_rand_point - transform.position).normalized;
    }


}
