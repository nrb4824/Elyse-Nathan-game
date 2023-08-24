using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WanderState : BaseState
{
    private Vector3? destination;   //nullable
    private float stopDistance = 1f;
    private float turnSpeed = 1f;
    private readonly LayerMask layerMask = LayerMask.NameToLayer("Walls");
    private float rayDistance = 3.5f;
    private Quaternion desiredRotation;
    private Vector3 direction;
    private Drone drone;

    public WanderState(Drone d) : base(d.gameObject)
    {
        drone = d;
    }

    public override Type Tick()
    {
        var chaseTarget = CheckForAggro();
        drone.Move();
        if (chaseTarget != null)
        {
            drone.SetTarget(chaseTarget);
            return typeof(ChaseState);
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
            transform.Translate(Vector3.forward * Time.deltaTime * EnemySettings.DroneSpeed);
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
                                new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f,
                                UnityEngine.Random.Range(-4.5f, 4.5f));

        destination = new Vector3(testPosition.x, 1f, testPosition.z);
        direction = Vector3.Normalize(destination.Value - transform.position);
        direction = new Vector3(direction.x, 0f, direction.z);
        desiredRotation = Quaternion.LookRotation(direction);
        Debug.Log("Got Direction");
    }

    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    private Transform CheckForAggro()
    {
        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var directionLocal = angle * Vector3.forward;
        var pos = transform.position;
        for (var i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, directionLocal, out hit, EnemySettings.AggroRadius))
            {
                var player = hit.collider.GetComponent<Player>();
                if (player != null)
                {
                    drone.SetPlayer(player);
                    return player.transform;
                }
                /*var drone = hit.collider.GetComponent<Drone>();
                if (drone != null && drone.GetTeam() != gameObject.GetComponent<Drone>().GetTeam())
                {
                    Debug.DrawRay(pos, directionLocal * hit.distance, Color.red);
                    return drone.transform;
                }*/
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(pos, directionLocal * EnemySettings.AggroRadius, Color.white);
            }
            directionLocal = stepAngle * directionLocal;
        }
        
        return null;

    }
}
