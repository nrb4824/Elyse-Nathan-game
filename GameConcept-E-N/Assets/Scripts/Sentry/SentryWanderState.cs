using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SentryWanderState : SentryBaseState
{
    private Vector3? destination;   //nullable
    private float stopDistance = 1f;
    private float turnSpeed = 1f;
    private readonly LayerMask layerMask = LayerMask.NameToLayer("Walls");
    private float rayDistance = 3.5f;
    private Quaternion desiredRotation;
    private Vector3 direction;
    private Sentry sentry;

    public SentryWanderState(Sentry s) : base(s.gameObject)
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
            return typeof(SentryChaseState);
        }

        if(destination.HasValue == false || Vector3.Distance(transform.position, destination.Value) <= stopDistance)
        {
            FindRandomDestination();
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * turnSpeed);

        if(IsForwardBlocked())
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 0.2f);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * SentryEnemySettings.SentrySpeed);
        }

        Debug.DrawRay(transform.position, direction * rayDistance, Color.green);
        while(IsPathBlocked())
        {
            FindRandomDestination();
            Debug.Log("Wall)");
        }
        return null;
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, 0.5f, rayDistance, layerMask);
    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, direction);
        return Physics.SphereCast(ray, 0.5f, rayDistance, layerMask);
    }

    private void FindRandomDestination()
    {
        Vector3 testPosition = (transform.position + (transform.forward * 4f)) +
                                new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), UnityEngine.Random.Range(-1.5f, 1.5f),
                                UnityEngine.Random.Range(-4.5f, 4.5f));

        destination = new Vector3(testPosition.x, testPosition.y, testPosition.z);
        direction = Vector3.Normalize(destination.Value - transform.position);
        direction = new Vector3(direction.x, direction.y, direction.z);
        desiredRotation = Quaternion.LookRotation(direction);
        Debug.Log("Got Direction");
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