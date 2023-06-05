using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeChaseState : BeeBaseState
{
    private Bee bee;

    public BeeChaseState(Bee b) : base(b.gameObject)
    {
        bee = b;
    }

    public override Type Tick()
    {
        if (bee.Target == null) return typeof(BeeWanderState);
        bee.Move();
        transform.LookAt(new Vector3(bee.Target.position.x, bee.transform.position.y, bee.Target.position.z));
        transform.Translate(Vector3.forward * Time.deltaTime * BeeEnemySettings.BeeSpeed);

        var Distance = Vector3.Distance(transform.position, bee.Target.transform.position);
        if(Distance <= BeeEnemySettings.AttackRange)
        {
            return typeof(BeeAttackState);
        }
        else if(Distance >= BeeEnemySettings.AggroRadius)
        {
            return typeof(BeeWanderState);
        }

        return null;

    }
}
