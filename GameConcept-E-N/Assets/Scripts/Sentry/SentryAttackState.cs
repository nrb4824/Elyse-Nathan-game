using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryAttackState : SentryBaseState
{
    private float attackReadyTimer;
    private Sentry sentry;

    public SentryAttackState(Sentry s) : base(s.gameObject)
    {
        sentry = s;
    }

    public override Type Tick()
    {
        if (sentry.Target == null) return typeof(SentryWanderState);

        attackReadyTimer -= Time.deltaTime;
        
        if(attackReadyTimer <= 0f)
        {
            Debug.Log("Attack!");
            attackReadyTimer = SentryEnemySettings.AttackSpeed;
            sentry.FireWeapon();
            sentry.SetTarget(null);
        }
        return null;
    }
}
