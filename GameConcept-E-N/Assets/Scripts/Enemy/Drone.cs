using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Drone : MonoBehaviour
{

    [SerializeField] private Team team;
    public Target t;
    //[SerializeField] private GameObject laserVisual;
    private Animator anim;
    public Transform Target { get; private set; }
    public Player playerObject { get; private set; }

    public Team GetTeam()
    {
        return team;
    }

    public EnemyStateMachine StateMachine => GetComponent<EnemyStateMachine>();

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            //Add new states here
            { typeof(WanderState), new WanderState(this)},
            { typeof(ChaseState), new ChaseState(this)},
            { typeof(AttackState), new AttackState(this)}
        };

        GetComponent<EnemyStateMachine>().SetStates(states);
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
        //laserVisual.transform.position = (Target.position + transform.position) / 2f;

        float distance = Vector3.Distance(Target.position, transform.position);
        //laserVisual.transform.localScale = new Vector3(.1f, .1f, distance);
        //laserVisual.SetActive(true);
        if(playerObject != null)
        {
            playerObject.TakeDamage(EnemySettings.Damage);
        }
        Attack();

        StartCoroutine(TurnOffLaser());
    }

    private IEnumerator TurnOffLaser()
    {
        yield return new WaitForSeconds(1.0f);
        //laserVisual.SetActive(false);
        
        // deal damage
    }
    public enum Team
    {
        Red,
        Blue
    }
}
