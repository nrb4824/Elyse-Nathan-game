using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SentrySearchState : SentryBaseState
{
    private Vector3? destination;   //nullable
    private float stopDistance = 1f;
    private float turnSpeed = 1f;
    private readonly LayerMask layerMask = LayerMask.NameToLayer("Walls");
    private float rayDistance = 3.5f;
    private Quaternion desiredRotation;
    private Vector3 direction;
    private Sentry sentry;

    public SentrySearchState(Sentry s) : base(s.gameObject)
    {
        sentry = s;
    }

    public override Type Tick()
    {
        var chaseTarget = CheckForAggro();
        //sentry.Move();
        if (chaseTarget != null)
        {
            sentry.SetTarget(chaseTarget);
            return typeof(SentrySpawnState);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * turnSpeed);

        Debug.DrawRay(transform.position, direction * rayDistance, Color.green);
        return null;
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, 0.5f, rayDistance, layerMask);
    }

    private Transform CheckForAggro()
    {
        RaycastHit hit;
        var pos = transform.position;
        Collider[] rangeChecks = Physics.OverlapSphere(pos, SentryEnemySettings.AggroRadius, SentryEnemySettings.TargetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - pos).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < SentryEnemySettings.Angle / 2)
            {
                float distanceToTarget = Vector3.Distance(pos, target.position);

                if (Physics.Raycast(pos, directionToTarget, out hit, SentryEnemySettings.AggroRadius))
                {
                    var player = hit.collider.GetComponent<Player>();
                    if (player != null)
                    {
                        sentry.SetPlayer(player);
                        return player.transform;
                    }
                    else
                    {
                        Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                    }
                }
            }
        }
        return null;

    }
}