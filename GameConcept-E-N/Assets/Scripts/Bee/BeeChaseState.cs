using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeChaseState : BeeBaseState
{
    private Bee bee;
    private readonly LayerMask layerMask = LayerMask.NameToLayer("Walls");
    private float rayDistance = 3.5f;
    private float turnSpeed = 1f;
    private bool blocked = false;
    private float blockedCount = 0f;

    public BeeChaseState(Bee b) : base(b.gameObject)
    {
        bee = b;
    }

    public override Type Tick()
    {
        if (bee.Target == null) return typeof(BeeWanderState);
        bee.Move();
        var direction = new Vector3(bee.Target.position.x, bee.Target.position.y, bee.Target.position.z);
        var desiredRotation = Quaternion.LookRotation(direction);

        if (IsForwardBlocked())
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.2f);
            blocked = true;
            blockedCount += 1;
            if (blockedCount > 10)
            {
                transform.Translate(-Vector3.forward * Time.deltaTime * 4* BeeEnemySettings.BeeSpeed);
                blockedCount = 0;
                //return typeof(BeeWanderState);
            }
        }
        else
        {
            if (blocked)
            {
                if (!IsLeftBlocked() && !IsRightBlocked() && !IsForwardBlocked())
                {
                    blockedCount = 0;
                    blocked = false;
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * turnSpeed);
                }
            }
            else
            {
                transform.LookAt(direction);
            }
            transform.Translate(Vector3.forward * Time.deltaTime * BeeEnemySettings.BeeSpeed);
            var Distance = Vector3.Distance(transform.position, bee.Target.transform.position);

            if (Distance <= BeeEnemySettings.AttackRange)
            {
                return typeof(BeeAttackState);
            }
            else if (Distance >= BeeEnemySettings.AggroRadius)
            {
                return typeof(BeeWanderState);
            }

            
        }
        return null;
       

    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, 0.5f, rayDistance, layerMask);
    }

    private bool IsLeftBlocked()
    {
        Ray ray = new Ray(transform.position, -transform.right);
        return Physics.SphereCast(ray, 0.5f, rayDistance, layerMask);
    }

    private bool IsRightBlocked()
    {
        Ray ray = new Ray(transform.position, transform.right);
        return Physics.SphereCast(ray, 0.5f, rayDistance, layerMask);
    }
   
}


