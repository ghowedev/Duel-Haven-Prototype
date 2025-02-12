using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_A1 : BaseAbility
{
    void Update()
    {

    }


    protected override void ApplyGameplayEffects()
    {

    }

    protected override void PlayEffects()
    {

    }

    protected override void StartAnimation()
    {
        base.animator.Play("BP_A1_down");
    }
}
