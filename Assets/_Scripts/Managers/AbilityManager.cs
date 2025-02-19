using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public Player player;
    public List<AbilitySO> abilitySOs;
    protected Dictionary<KeyCode, BaseAbility> abilityBindings = new Dictionary<KeyCode, BaseAbility>();
    private List<BaseAbility> abilityInstances = new List<BaseAbility>();
    private List<BaseAbility> activeAbilities = new List<BaseAbility>();
    // private List<BaseAbility> activeAbilitiesToUpdate = new List<BaseAbility>();

    #region Start/Initialize
    void Start()
    {
        InitializeAbilities();


    }

    protected void InitializeAbilities()
    {
        foreach (var abilityName in abilitySOs)
        {
            // Dynamically load the class (as a type) based on the ability name
            // Type abilityType = Type.GetType(abilityName);
            Type abilityType = abilityName.GetAbilityType();

            if (abilityType == null || !typeof(BaseAbility).IsAssignableFrom(abilityType))
            {
                Debug.LogWarning("Ability " + abilityName + " not found or is not of type BaseAbility.");
                return;
            }

            // Add the ability as a component to the player
            BaseAbility ability = player.gameObject.AddComponent(abilityType) as BaseAbility;
            abilityInstances.Add(ability);
            ability.Initialize(abilityName);

            // Add the ability to the keybinding dictionary if it has a keybind
            if (ability != null && ability.abilityData.keybind != KeyCode.None && !abilityBindings.ContainsKey(ability.abilityData.keybind))
            {
                abilityBindings.Add(ability.abilityData.keybind, ability);
            }



        }
    }
    #endregion

    protected virtual void Update()
    {
        foreach (var binding in abilityBindings)
        {
            if (!CanUse()) continue;

            if (Input.GetKeyDown(binding.Key))
                binding.Value.UseAbility();
            else if (Input.GetKey(binding.Key))
                binding.Value.UpdateAbility();
            else if (Input.GetKeyUp(binding.Key))
                binding.Value.ReleaseAbility();

        }
        // UpdateActiveAbilities();
        UpdateCooldowns();
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

    #region Resource Checks
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
    #endregion

    private void UpdateCooldowns()
    {
        foreach (var binding in abilityBindings.Values)
        {
            binding.UpdateCooldown(Time.deltaTime);
        }
    }
}

// private void UpdateActiveAbilities()
// {
//     // Remove finished abilities
//     activeAbilities.RemoveAll(ability => !ability.isActive);

//     foreach (var ability in activeAbilities)
//     {
//         ability.UpdateAbility();
//     }
// }