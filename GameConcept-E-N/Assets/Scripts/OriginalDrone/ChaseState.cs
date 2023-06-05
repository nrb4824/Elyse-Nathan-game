using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    private Drone drone;

    public ChaseState(Drone d) : base(d.gameObject)
    {
        drone = d;
    }

    public override Type Tick()
    {
        if (drone.Target == null) return typeof(WanderState);
        drone.Move();
        transform.LookAt(new Vector3(drone.Target.position.x, drone.transform.position.y, drone.Target.position.z));
        transform.Translate(Vector3.forward * Time.deltaTime * EnemySettings.DroneSpeed);

        var Distance = Vector3.Distance(transform.position, drone.Target.transform.position);
        if(Distance <= EnemySettings.AttackRange)
        {
            return typeof(AttackState);
        }
        else if(Distance >= EnemySettings.AggroRadius)
        {
            return typeof(WanderState);
        }

        return null;

    }
}
