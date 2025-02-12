using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : MonoBehaviour
{
    protected AbilitySO _abilityData;
    public AbilitySO abilityData => _abilityData;
    protected Animator animator;

    protected float currentCooldown;
    protected bool isOnCooldown;

    protected virtual void Start()
    {
        animator = GetComponentInParent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }
    public virtual bool isActive { get; protected set; }

    public virtual void UseAbility()
    {
        StartAnimation();
        PlayEffects();
        ApplyGameplayEffects();
        StartCooldown();
        // _abilityData.SpawnProjectile(player);
    }

    protected abstract void StartAnimation();
    protected abstract void PlayEffects();
    protected abstract void ApplyGameplayEffects();

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
