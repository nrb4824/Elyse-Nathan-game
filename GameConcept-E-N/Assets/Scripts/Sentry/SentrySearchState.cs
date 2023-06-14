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
    private Quaternion roundedRotation;
    private Vector3 direction;
    private bool start = true;
    private Sentry sentry;
    private bool turned = false;
    private float tolerance = .05f;

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
            Debug.Log("found");
            sentry.SetTarget(chaseTarget);
            return typeof(SentrySpawnState);
        }
        var difference = desiredRotation.y - sentry.transform.rotation.y;
        if (start)
        {
            Debug.Log("Scanning");
            start = false;
            Scan();
        }
        else if(!turned && difference < tolerance)
        {
            Debug.Log("Scanning");
            Scan();
        }
        else if(turned && difference > -tolerance)
        {
            Debug.Log("Scanning");
            Scan();
        }
        transform.rotation = Quaternion.Lerp(sentry.transform.rotation, desiredRotation, Time.deltaTime * SentryEnemySettings.RotateSpeed);
        return null;
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, 0.5f, rayDistance, layerMask);
    }

    private void Scan()
    {
        if (turned)
        {
            turned = false;
            desiredRotation = Quaternion.Euler(new Vector3(sentry.xAngle, sentry.yAngle + sentry.scanAngle/2, sentry.zAngle));
        }
        else if (!turned)
        {
            turned = true;
            desiredRotation = Quaternion.Euler(new Vector3(sentry.xAngle, sentry.yAngle - sentry.scanAngle/2, sentry.zAngle));
        }
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