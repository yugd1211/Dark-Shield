using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState CurState { get; private set; }

    public IdleState idleState;
    public WalkState walkState;
    public Skill1State skill1State;
    public Skill2State skill2State;
    public DashState dashState;
    public DieState dieState;
    public HitState hitState;

    public event Action<IState> StateChanged;

    public StateMachine(Player player)
    {
        idleState = new IdleState(player);
        walkState = new WalkState(player);
        skill1State = new Skill1State(player);
        skill2State = new Skill2State(player);
        dashState = new DashState(player);
        dieState = new DieState(player);
        hitState = new HitState(player);
    }

    //On Start, IdleState
    public void Init(IState state)
    {
        CurState = state;
        state.OnEnter();

        StateChanged?.Invoke(state);
    }

    public void TransitionTo(IState nextState)
    {
        CurState.OnExit();
        CurState = nextState;
        nextState.OnEnter();

        StateChanged?.Invoke(nextState);
    }

    public void OnUpdate()
    {
        if (CurState != null)
        {
            CurState.OnUpdate();
        }
    }

    public bool CanEnterHitState()
    {
        if (CurState == idleState || CurState == walkState) return true;
        return false;
    }
}
