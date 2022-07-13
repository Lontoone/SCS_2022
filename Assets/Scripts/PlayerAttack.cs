using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask attack_target_layer;//攻擊layer
    public float baseDamage = 5;//基本攻擊力
    public float attackRange = 2; //攻擊範圍
    public ActionController.mAction attack;
    ActionController actionController;
    public KeyCode attackKeyCode;
    public ColliderDetector colliderDetector;

    public Collider attackRangeCollider;
    Animator animator;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        actionController = gameObject.GetComponent<ActionController>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(attackKeyCode))
        {
            actionController.AddAction(attack);
        }
    }
    public void Attack()
    {
        Debug.Log("Attack");
        animator.Play("Attack");
        //Attack_anima_event(10);
        for (int i=0; i< colliderDetector.collidersInRange.Count; i++) {
            HitableObject.Hit_event_c(colliderDetector.collidersInRange[i], 10);
        
        }
    }

    public void Attack_anima_event(float damage_multiplier)//傳入攻擊乘數 (由Animator呼叫)
    {
        Collider[] objs = Physics.OverlapSphere(transform.position, attackRange, attack_target_layer);
        foreach (Collider c in objs)
        {
            if (MyTool.IsInside(attackRangeCollider, c.transform.position))
                HitableObject.Hit_event_c(c.gameObject, damage_multiplier * baseDamage);
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

  
}
