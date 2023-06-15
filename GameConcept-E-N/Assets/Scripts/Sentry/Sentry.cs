using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sentry : MonoBehaviour
{

    [SerializeField] private Team team;
    private Animator anim;
    public Transform Target { get; private set; }
    public Player playerObject { get; private set; }
    public Light light;

    public GameObject Bee;
    public int beeNumber;
    public Transform playerCam;
    public float scanAngle;
    public float xAngle;
    public float yAngle;
    public float zAngle;

    public Team GetTeam()
    {
        return team;
    }

    public SentryEnemyStateMachine StateMachine => GetComponent<SentryEnemyStateMachine>();

    private void Awake()
    {
        //anim = gameObject.GetComponent<Animator>();
        light.spotAngle = SentryEnemySettings.Angle;
        light.innerSpotAngle = SentryEnemySettings.Angle;
        light.range = SentryEnemySettings.AggroRadius;
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, SentryBaseState>()
        {
            //Add new states here
            { typeof(SentrySearchState), new SentrySearchState(this)},
            { typeof(SentrySpawnState), new SentrySpawnState(this)}
        };

        GetComponent<SentryEnemyStateMachine>().SetStates(states);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public void SetPlayer(Player player)
    {
        playerObject = player;
    }

    public void Move()
    {
        anim.SetBool("Forward", true);
        anim.SetBool("Attack", false);
    }

    public void Attack()
    {
        anim.SetBool("Attack", true);
        anim.SetBool("Forward", false);
    }


    public void FireWeapon()
    {
        float distance = Vector3.Distance(Target.position, transform.position);
        var direction = new Vector3(this.Target.position.x, this.Target.position.y, this.Target.position.z);

        StartCoroutine(TurnOffLaser());
    }

    private IEnumerator TurnOffLaser()
    {
        yield return new WaitForSeconds(.2f);
        
        // deal damage
    }
    public enum Team
    {
        Red,
        Blue
    }
    public void setCamera(Transform cam)
    {
        this.playerCam = cam;
    }
}
