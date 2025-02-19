using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private PlayerState currentState = PlayerState.IDLE;
    private CastType castChargeChannelType;
    private float chargeTime;
    private DisabledType disabledType;
    private float disabledDuration;
    private Vector2 disabledDirection;
    private float disabledSpeed;
    public bool CanMove { get; private set; } = true;

    public bool ChangeState(PlayerState newState)
    {
        if (!CanTransitionTo(newState)) return false;

        OnStateExit(currentState);
        currentState = newState;
        OnStateEnter(newState);

        return true;
    }

    private bool CanTransitionTo(PlayerState newState)
    {
        // transition rules here
        if (currentState == PlayerState.DISABLED && newState != PlayerState.IDLE)
            return false;

        return true;

    }

    private void OnStateEnter(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.IDLE:
                CanMove = true;
                break;
            case PlayerState.CASTING:
                CanMove = false;
                break;
            case PlayerState.DISABLED:
                CanMove = false;
                break;
        }
    }

    private void OnStateUpdate(PlayerState state)
    {

    }

    private void OnStateExit(PlayerState state)
    {

    }

    public PlayerState GetCurrentState()
    {
        return currentState;
    }

    void Update()
    {
        OnStateUpdate(currentState);
    }
}