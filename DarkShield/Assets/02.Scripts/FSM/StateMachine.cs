using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState CurState { get; private set; }

    public IdleState idleState;
    public WalkState walkState;
    public SkillState skillState;
    public DashState dashState;
    public DieState dieState;
    public HitState hitState;

    public event Action<IState> StateChanged;

    public bool IsTransitioning { get; private set; }

    public StateMachine(Player player)
    {
        idleState = new IdleState(player);
        walkState = new WalkState(player);
        skillState = new SkillState(player);
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
        IsTransitioning = true;
        CurState.OnExit();
        CurState = nextState;
        nextState.OnEnter();
        IsTransitioning = false;

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
