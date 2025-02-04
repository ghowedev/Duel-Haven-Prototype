using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class AbilitySO : ScriptableObject
{
    public string abilityName;
    public Sprite icon;
    public KeyCode keybind;
    public float cooldown;
    public int damage;
    public int energyCost;




    //optionals
    public Animation animation;
    public float range;
    public CastType castType;
    public float castTime;
    public CastData castData;
    public ProjectileData projectileData;
    public DisabledData disabledData;
    public SlowData slowData;
}

//----------------------------------------
// ----------Sub data structures----------
//----------------------------------------

[Serializable]
public class ProjectileData
{
    public float speed;
    public float priority;
    public GameObject prefab;
}

[Serializable]
public class CastData
{
    public CastType castType = CastType.NONE;
    public float minChargeTime;
    public float maxChargeTime;
    public int minDamage;
    public int maxDamage;
    public float chargeRate;
}
[Serializable]
public class DisabledData
{
    public DisabledType disabledType = DisabledType.NONE;
    public float duration;
    public float knockbackForce;
    public int breakAmount;
}

[Serializable]
public class SlowData
{
    public float amount;
    public float duration;
    public float decay;
}