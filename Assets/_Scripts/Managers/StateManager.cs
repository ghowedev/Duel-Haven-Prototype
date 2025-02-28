using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private Animator animator;
    private PlayerState currentState = PlayerState.IDLE;
    private CastType castChargeChannelType;
    private float chargeTime;
    private DisabledType disabledType;
    private float disabledDuration;
    private Vector2 disabledDirection;
    private float disabledSpeed;
    public bool CanMove { get; private set; } = true;
    private Dictionary<PlayerState, int> stateAnimatorInt = new Dictionary<PlayerState, int>
{
    { PlayerState.IDLE, 0 },      // IDLE maps to 0
    { PlayerState.CASTING, 1 },   // CASTING maps to 1
    { PlayerState.DISABLED, 2 }   // DISABLED maps to 2
};


    void Start()
    {
        animator = GetComponentInParent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    public bool ChangeState(PlayerState newState)
    {
        if (!CanTransitionTo(newState)) return false;

        OnStateExit(currentState);
        currentState = newState;
        OnStateEnter(newState);

        animator.SetInteger("State", stateAnimatorInt[currentState]);

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