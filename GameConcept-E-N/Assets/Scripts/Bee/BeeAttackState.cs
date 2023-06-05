using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAttackState : BeeBaseState
{
    private float attackReadyTimer;
    private Bee bee;

    public BeeAttackState(Bee b) : base(b.gameObject)
    {
        bee = b;
    }

    public override Type Tick()
    {
        if (bee.Target == null) return typeof(BeeWanderState);

        attackReadyTimer -= Time.deltaTime;
        
        if(attackReadyTimer <= 0f)
        {
            Debug.Log("Attack!");
            attackReadyTimer = BeeEnemySettings.AttackSpeed;
            bee.FireWeapon();
            bee.SetTarget(null);
        }
        return null;
    }
}
