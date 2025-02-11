using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public Player player;
    protected Dictionary<KeyCode, AbilitySO> abilityBindings = new Dictionary<KeyCode, AbilitySO>();
    public List<AbilitySO> abilitySO = new List<AbilitySO>();

    #region Start/Initialize
    void Start()
    {
        InitializeAbilities();
    }

    protected void InitializeAbilities()
    {
        foreach (var ability in this.abilitySO)
        {
            if (abilityBindings.ContainsKey(ability.keybind) == false)
                abilityBindings.Add(ability.keybind, ability);
        }
    }
    #endregion

    protected virtual void Update()
    {
        foreach (var binding in abilityBindings)
        {
            binding.Value.UpdateAbility(player);

            if (Input.GetKeyDown(binding.Key) && CanUse())
            {
                binding.Value.UseAbility(player);
            }

            if (Input.GetKey(binding.Key))
            {
                binding.Value.UpdateAbility(player);
            }

            if (Input.GetKeyUp(binding.Key))
            {
                binding.Value.ReleaseAbility(player);
            }
        }
        // UpdateActiveAbilities();
        UpdateCooldowns();
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