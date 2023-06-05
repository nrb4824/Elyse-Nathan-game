using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeeEnemyStateMachine : MonoBehaviour
{
    private Dictionary<Type, BeeBaseState> availableStates;

    public BeeBaseState CurrentState { get; private set; }
    public event Action<BeeBaseState> OnStateChanged;

    public void SetStates(Dictionary<Type, BeeBaseState> states)
    {
        availableStates = states;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentState == null)
        {
            CurrentState = availableStates.Values.First();
        }

        var nextState = CurrentState?.Tick(); //?. means not null
        if(nextState != null && nextState != CurrentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState = availableStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
    }
}
