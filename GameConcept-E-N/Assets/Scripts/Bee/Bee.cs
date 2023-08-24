using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bee : MonoBehaviour
{

    [SerializeField] private Team team;
    private GameObject laserVisual;
    //private Animator anim;
    public Transform Target { get; private set; }
    public Player playerObject { get; private set; }
    private GameObject laser;

    public Team GetTeam()
    {
        return team;
    }

    public BeeEnemyStateMachine StateMachine => GetComponent<BeeEnemyStateMachine>();

    private void Awake()
    {
        //anim = gameObject.GetComponent<Animator>();
        laserVisual = BeeEnemySettings.BeeProjectilePrefab;
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BeeBaseState>()
        {
            //Add new states here
            { typeof(BeeWanderState), new BeeWanderState(this)},
            { typeof(BeeChaseState), new BeeChaseState(this)},
            { typeof(BeeAttackState), new BeeAttackState(this)}
        };

        GetComponent<BeeEnemyStateMachine>().SetStates(states);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public void SetPlayer(Player player)
    {
        playerObject = player;
    }

    /*public void Move()
    {
        anim.SetBool("Forward", true);
        anim.SetBool("Attack", false);
    }

    public void Attack()
    {
        anim.SetBool("Attack", true);
        anim.SetBool("Forward", false);
    }*/


    public void FireWeapon()
    {
        laser = Instantiate(laserVisual, this.transform.position, this.transform.rotation) as GameObject;
        

        float distance = Vector3.Distance(Target.position, transform.position);
        laser.transform.localScale = new Vector3( 0.1f, distance, .1f);
        var direction = new Vector3(this.Target.position.x, this.Target.position.y, this.Target.position.z);
        laser.transform.LookAt(direction);
        laser.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);

        if (playerObject != null)
        {
            playerObject.TakeDamage(BeeEnemySettings.Damage);
        }
        //Attack();

        StartCoroutine(TurnOffLaser());
    }

    private IEnumerator TurnOffLaser()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(laser);
        
        // deal damage
    }
    public enum Team
    {
        Red,
        Blue
    }
}
