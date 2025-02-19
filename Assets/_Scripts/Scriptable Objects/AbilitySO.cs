using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability/New Ability")]
public class AbilitySO : ScriptableObject
{
    public string abilityType;
    public Sprite icon;
    public KeyCode keybind;
    public float cooldown;
    public int energyCost;
    public Animation animation;
    public float range;
    public float castTime;
    public DamageDataSO damageDataSO;
    public CastDataSO castData;
    public ProjectileDataSO projectileData;


    public Type GetAbilityType()
    {
        return Type.GetType(abilityType);
    }
}
// public DisabledData disabledData;
// public SlowData slowData;
/*
    public void SpawnProjectile(Player player)
    {
        if (projectileData)
        {
            projectileData.Spawn();
        }
    }

    public void UpdateAbility(Player player)
    {

    }

    public void UseAbility(Player player)
    {
        SpawnProjectile(player);
    }

    public void ReleaseAbility(Player player)
    {

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

}
*/

//----------------------------------------
// ----------Sub data structures----------
//----------------------------------------


// [Serializable]
// public class DisabledData
// {
//     public DisabledType disabledType = DisabledType.NONE;
//     public float duration;
//     public float knockbackForce;
//     public int breakAmount;
// }

// [Serializable]
// public class SlowData
// {
//     public float amount;
//     public float duration;
//     public float decay;
// }