using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryChaseState : SentryBaseState
{
    private Sentry sentry;
    private readonly LayerMask layerMask = LayerMask.NameToLayer("Walls");
    private float rayDistance = 3.5f;
    private float turnSpeed = 1f;
    private bool blocked = false;
    private float blockedCount = 0f;

    public SentryChaseState(Sentry s) : base(s.gameObject)
    {
        sentry = s;
    }

    public override Type Tick()
    {
        if (sentry.Target == null) return typeof(SentryWanderState);
        //sentry.Move();
        var direction = new Vector3(sentry.Target.position.x, sentry.Target.position.y, sentry.Target.position.z);
        var desiredRotation = Quaternion.LookRotation(direction);

        if (IsForwardBlocked())
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.2f);
            blocked = true;
            blockedCount += 1;
            if (blockedCount > 10)
            {
                transform.Translate(-Vector3.forward * Time.deltaTime * 4* SentryEnemySettings.SentrySpeed);
                blockedCount = 0;
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
            transform.Translate(Vector3.forward * Time.deltaTime * SentryEnemySettings.SentrySpeed);
            var Distance = Vector3.Distance(transform.position, sentry.Target.transform.position);

            if (Distance <= SentryEnemySettings.AttackRange)
            {
                return typeof(SentryAttackState);
            }
            else if (Distance >= SentryEnemySettings.AggroRadius)
            {
                return typeof(SentryWanderState);
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


