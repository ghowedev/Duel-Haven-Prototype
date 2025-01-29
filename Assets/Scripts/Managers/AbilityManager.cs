using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityManager : MonoBehaviour
{
    protected Dictionary<KeyCode, BaseAbility> abilityBindings = new Dictionary<KeyCode, BaseAbility>();
    protected List<BaseAbility> activeAbilities = new List<BaseAbility>();

    protected abstract void InitializeAbilities();
    protected abstract bool CanUseAbilities();

    protected virtual void Update()
    {
        foreach (var binding in abilityBindings)
        {
            if (Input.GetKeyDown(binding.Key) && CanUseAbilities())
            {
                binding.Value.UseAbility();
                activeAbilities.Add(binding.Value);
            }

            if (Input.GetKey(binding.Key))
            {
                binding.Value.UpdateAbility();
            }

            if (Input.GetKeyUp(binding.Key))
            {
                binding.Value.ReleaseAbility();
            }
        }

        UpdateActiveAbilities();
        UpdateCooldowns();
    }

    private bool CanUse()
    {
        return HasResources() && CooldownIsZero() && ActionableState();
    }

    protected virtual bool HasResources()
    {
        return true;
    }

    protected virtual bool CooldownIsZero()
    {
        return true;
    }

    protected virtual bool ActionableState()
    {
        return true;
    }

    private void UpdateActiveAbilities()
    {
        // Remove finished abilities
        activeAbilities.RemoveAll(ability => !ability.isActive);

        foreach (var ability in activeAbilities)
        {
            ability.UpdateAbility();
        }
    }

    private void UpdateCooldowns()
    {
        foreach (var binding in abilityBindings.Values)
        {
            binding.UpdateCooldown(Time.deltaTime);
        }
    }
}
