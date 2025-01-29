using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerAbilityManager : AbilityManager
{
    private AbilitySO stabSO;
    protected override void InitializeAbilities()
    {
        StabAbility stab = gameObject.AddComponent<StabAbility>();
        stabSO = Resources.Load<AbilitySO>("Abilities/Berserker/StabSO");
        stab.abilitySO = stabSO;
        abilityBindings.Add(stab.abilitySO.keybind, stab);
    }

    protected override bool CanUseAbilities()
    {
        return true;
    }

    void Start()
    {
        InitializeAbilities();
    }
}
