using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : MonoBehaviour
{
    protected AbilitySO _abilityData;
    public AbilitySO abilityData => _abilityData;
    protected Animator animator;
    protected StateManager stateManager;
    protected AnimationEventDispatcher animationEventDispatcher;
    protected SoundEmitter soundEmitterOneShot;
    protected SoundEmitter soundEmitterLooping;

    protected float currentCooldown;
    protected bool isOnCooldown;
    public virtual bool isActive { get; protected set; }

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
            Debug.LogError("State Manager component not found on " + gameObject.name);
        }

        animationEventDispatcher = GetComponentInParent<AnimationEventDispatcher>();
        if (animationEventDispatcher == null)
        {
            Debug.LogError("Animation Event Dispatcher component not found on " + gameObject.name);
        }

        soundEmitterOneShot = GetComponentInParent<SoundEmitter>();
        soundEmitterLooping = GetComponentInParent<SoundEmitter>();
        if (soundEmitterOneShot == null || soundEmitterLooping == null)
        {
            Debug.Log(soundEmitterOneShot);
            Debug.Log(soundEmitterLooping);
        }
    }

    public void Initialize(AbilitySO data)
    {
        this._abilityData = data;
    }


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

    public virtual void UseAbility() { }
    public virtual void UpdateAbility() { }
    protected abstract void StartAnimation();
    protected abstract void PlayFX();
    protected abstract void PlayAudio();
    protected abstract void ApplyCombatEffects();
    public abstract void Interrupt();
    protected virtual void Cleanup() { }
    public virtual void FixedUpdateAbility() { }
    public virtual void ReleaseAbility() { }
    protected virtual void OnAnimationComplete() { }
}
