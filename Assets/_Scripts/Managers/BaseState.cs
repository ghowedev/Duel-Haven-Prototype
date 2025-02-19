/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected PlayerState currentState = PlayerState.IDLE;
    protected CastType castType;
    protected DisabledType disabledType;

    protected float chargeTime;
    protected float disabledDuration;
    protected Vector2 disabledDirection;
    protected float disabledSpeed;

    public bool CanMove { get; protected set; } = true;
    public bool CanCast { get; protected set; } = true;

    public virtual bool ChangeState(PlayerState newState)
    {
        if (!CanTransitionTo(newState)) return false;

        OnStateExit(currentState); // Clean up the current state
        currentState = newState;   // Update the state
        OnStateEnter(newState);    // Initialize the new state

        return true;
    }

    protected virtual bool CanTransitionTo(PlayerState newState)
    {
        // Add base transition rules here
        if (currentState == PlayerState.DISABLED && newState != PlayerState.IDLE)
            return false; // Can't transition out of DISABLED except to IDLE

        return true; // Default: allow the transition
    }

    protected virtual void OnStateEnter(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.IDLE:
                CanMove = true;
                CanCast = true;
                break;
            case PlayerState.CASTING:
                CanMove = false;
                CanCast = false;
                break;
            case PlayerState.DISABLED:
                CanMove = false;
                CanCast = false;
                break;
        }
    }

    protected virtual void OnStateExit(PlayerState state)
    {

    }

    protected virtual void UpdateState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.IDLE:
                break;
            case PlayerState.CASTING:

                break;
            case PlayerState.DISABLED:

                break;
        }
    }

    // protected virtual void HandleMovement() { }
    // protected virtual void HandleAttack() { }
    // protected virtual void HandleCasting() { }
    // protected virtual void HandleDisabled() { }

    void Update()
    {
        UpdateState(currentState);
    }
}
*/