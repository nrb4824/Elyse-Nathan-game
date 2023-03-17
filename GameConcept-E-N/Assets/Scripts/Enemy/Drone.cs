using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Drone : MonoBehaviour
{

    [SerializeField] private Team team;
    [SerializeField] private GameObject laserVisual;
    public Transform Target { get; private set; }

    public Team GetTeam()
    {
        return team;
    }

    public EnemyStateMachine StateMachine => GetComponent<EnemyStateMachine>();

    private void Awake()
    {
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

    public void FireWeapon()
    {
        laserVisual.transform.position = (Target.position + transform.position) / 2f;

        float distance = Vector3.Distance(Target.position, transform.position);
        laserVisual.transform.localScale = new Vector3(.1f, .1f, distance);
        laserVisual.SetActive(true);

        StartCoroutine(TurnOffLaser());
    }

    private IEnumerator TurnOffLaser()
    {
        yield return new WaitForSeconds(0.25f);
        laserVisual.SetActive(false);

        if(Target != null)
        {
            GameObject.Destroy(Target.gameObject);
        }
    }
    public enum Team
    {
        Red,
        Blue
    }
}
