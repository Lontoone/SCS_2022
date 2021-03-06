using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitableObject : MonoBehaviour
{
    public static event Action<Vector3, float> HitEffect_event;
    private static event Action<GameObject, float> Hit_event;

    private static event Action<GameObject, float> Heal_event;
    public event Action Die_event;
    public event Action gotHit_event;
    public event Action gotHeal_event;
    public float HP = 20;
    public bool isDead = false;
    public bool isHitable = true;

    public float hitInterval = 0.5f;
    private float lastHurtTime = 0;

    private void Start()
    {
        Hit_event += Hit;
        Heal_event += Heal;
        lastHurtTime = Time.time;
    }
    private void OnDestroy()
    {
        Hit_event -= Hit;

        Heal_event -= Heal;
    }

    public static void Heal_event_c(GameObject t, float d) //CALL THIS
    {
        if (Heal_event != null)
        {
            Heal_event(t, d);
        }
    }

    public static void Hit_event_c(GameObject t, float d) //CALL THIS
    {
        if (Hit_event != null)
        {
            Hit_event(t, d);
        }
    }

    void Heal(GameObject target, float damage)
    {
        if (target == gameObject)
        {
            HP += damage;
        }
        gotHeal_event?.Invoke();
    }

    void Hit(GameObject target, float damage)
    {
        if (target == gameObject)
        {
            //冷卻
            if (Time.time - lastHurtTime > hitInterval)
            {
                lastHurtTime = Time.time;
            }
            else
            {
                return;
            }

            Debug.Log(gameObject.name + " 受到 " + damage + " 傷害");

            if (isHitable)
            {
                HP -= damage;
                //特效:
                HitEffect_event?.Invoke(target.transform.position, damage);
            }
            //判斷死亡
            if (HP <= 0)
            {
                if (Die_event != null && !isDead)
                {
                    isDead = true;
                    Die_event();
                }
            }
            else
            {
                if (gotHit_event != null)
                {
                    Debug.Log("<color=green>HURT</color>");
                    gotHit_event();
                }
            }

        }
    }

    void Hit_effect()
    {
        //TODO: effect
    }
}

