using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_A1 : BaseAbility
{
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
        stateManager.ChangeState(PlayerState.CASTING);
        PlayAudio();
    }

    public override void ReleaseAbility()
    {
        stateManager.ChangeState(PlayerState.IDLE);
    }

    protected override void PlayAudio()
    {

    }

    protected override void StartAnimation()
    {
        animator.Play("BP_A1_down");
    }
}
