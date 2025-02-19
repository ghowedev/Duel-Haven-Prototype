using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : MonoBehaviour
{
    protected AbilitySO _abilityData;
    public AbilitySO abilityData => _abilityData;
    protected Animator animator;
    protected StateManager stateManager;

    protected float currentCooldown;
    protected bool isOnCooldown;

    protected virtual void Start()
    {
        animator = GetComponentInParent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
        stateManager = GetComponentInParent<StateManager>();
        if (stateManager == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    public void Initialize(AbilitySO data)
    {
        this._abilityData = data;
    }
    public virtual bool isActive { get; protected set; }

    public virtual void UseAbility()
    {

    }

    protected abstract void StartAnimation();
    protected abstract void PlayFX();
    protected abstract void PlayAudio();
    protected abstract void ApplyCombatEffects();

    public void StartCooldown()
    {
        currentCooldown = _abilityData.cooldown;
        isOnCooldown = true;
    }

    public void UpdateCooldown(float deltaTime)
    {
        if (isOnCooldown)
        {
            currentCooldown -= deltaTime;
            if (currentCooldown <= 0)
            {
                currentCooldown = 0;
                isOnCooldown = false;
            }
        }
    }

    public virtual void Interrupt()
    {

    }

    protected virtual void Cleanup()
    {

    }

    public virtual void UpdateAbility()
    {

    }

    public virtual void FixedUpdateAbility()
    {

    }

    public virtual void ReleaseAbility()
    {

    }
}
