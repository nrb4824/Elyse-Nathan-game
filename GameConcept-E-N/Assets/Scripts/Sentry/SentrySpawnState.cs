using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentrySpawnState : SentryBaseState
{
    private float attackReadyTimer;
    private Sentry sentry;
    private GameObject bee;

    public SentrySpawnState(Sentry s) : base(s.gameObject)
    {
        sentry = s;
    }

    public override Type Tick()
    {
        if (sentry.Target == null) return typeof(SentrySearchState);

        attackReadyTimer -= Time.deltaTime;
        if (attackReadyTimer <= 0f)
        {
            Debug.Log("Attack!");
            attackReadyTimer = SentryEnemySettings.AttackSpeed;
            var offsetPos = this.sentry.beeNumber;
            for (int i = 0; i < this.sentry.beeNumber; i++)
            {
                var direction = new Vector3(this.sentry.Target.position.x, this.sentry.Target.position.y, this.sentry.Target.position.z);
                var pos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 2*i);
                this.bee = GameObject.Instantiate(this.sentry.Bee, pos, this.transform.rotation) as GameObject;
                this.bee.transform.LookAt(direction);
                Target t = this.bee.GetComponent<Target>();
                t.setCamera(this.sentry.playerCam);
            }
            sentry.SetTarget(null);
            Target target = gameObject.GetComponent<Target>();
            target.Die();
        }
        return null;
    }
}
