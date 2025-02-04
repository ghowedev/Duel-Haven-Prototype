using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabAbility : BaseAbility
{
    private float currentChargeTime = 0f;
    private string isCharging = "isCharging";
    private string stab = "Stab";

    protected override void Start()
    {
        base.Start();
        animator.SetBool(isCharging, false);
    }

    public override void UpdateAbility()
    {
        if (animator.GetBool(isCharging) && currentChargeTime >= abilitySO.castData.maxChargeTime)
        {
            ReleaseAbility();
        }

        if (animator.GetBool(isCharging) && isActive)
        {
            currentChargeTime += Time.deltaTime;
        }
    }

    public override void UseAbility()
    {
        Debug.Log("Charging!");

        isActive = true;
        currentChargeTime = 0f;

        StartAnimation();
        PlayEffects();
    }

    public override void ReleaseAbility()
    {
        if (animator.GetBool(isCharging) && isActive)
        {
            Debug.Log("Stab! Damage: " + CalculateDamage());

            StartAnimation();
            ApplyGameplayEffects();
            StartCooldown();

            isActive = false;
            currentChargeTime = 0f;
        }
    }

    protected override void StartAnimation()
    {
        if (!animator.GetBool(isCharging))
        {
            animator.SetBool(isCharging, true);
        }
        else
        {
            animator.SetBool(isCharging, false);
            animator.SetTrigger(stab);
        }
    }

    protected override void ApplyGameplayEffects()
    {
        int damage = CalculateDamage();
        // Apply damage - you'll need references to:
        // - Attack hitbox/range
        // - Target detection
        // - Damage application system
    }

    protected override void PlayEffects() { }

    private int CalculateDamage()
    {
        float damage = Mathf.Lerp(abilitySO.castData.minDamage,
                         abilitySO.castData.maxDamage,
                         currentChargeTime / abilitySO.castData.maxChargeTime);
        return (int)damage;
    }
}
