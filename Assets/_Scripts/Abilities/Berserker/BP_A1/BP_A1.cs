using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_A1 : BaseAbility
{
    private Coroutine animationCoroutine;

    protected override void ApplyCombatEffects()
    {

    }

    protected override void PlayFX()
    {

    }

    public override void UpdateAbility()
    {

    }

    public override void UseAbility()
    {
        // animationEventDispatcher.RegisterCallback("BP_A1_Complete", OnAnimationComplete);
        stateManager.ChangeState(PlayerState.CASTING);
        PlayAudio();
        StartAnimation();
    }

    public override void ReleaseAbility() { }

    protected override void PlayAudio()
    {
        Debug.Log("Audio played");
    }

    protected override void StartAnimation()
    {
        // animator.SetInteger("Direction", 2);
        // animator.SetInteger("Ability", 1);
        // animator.SetTrigger("A1");
        animator.Play("BP_A1_down");
        animationCoroutine = StartCoroutine(ReturnToIdleAfterAnimation());
    }

    private IEnumerator ReturnToIdleAfterAnimation()
    {
        //TODO: CACHE CLIP LENGTH IN SCRIPTABLE OBJECT
        yield return new WaitForSeconds(_abilityData.animationClipLength);
        stateManager.ChangeState(PlayerState.IDLE);
    }

    public override void Interrupt()
    {
        StopCoroutine(animationCoroutine);
        // Should combat manager handle this state change and the stun timer?
        // stateManager.ChangeState(PlayerState.DISABLED);
    }
}
