using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float attackReadyTimer;
    private Drone drone;

    public AttackState(Drone d) : base(d.gameObject)
    {
        drone = d;
    }

    public override Type Tick()
    {
        if (drone.Target == null) return typeof(WanderState);

        attackReadyTimer -= Time.deltaTime;
        
        if(attackReadyTimer <= 0f)
        {
            Debug.Log("Attack!");
            attackReadyTimer = EnemySettings.AttackSpeed;
            drone.FireWeapon();
            drone.SetTarget(null);
        }
        return null;
    }
}
