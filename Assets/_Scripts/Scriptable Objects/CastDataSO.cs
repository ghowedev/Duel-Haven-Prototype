using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Ability", menuName = "Ability/CastData")]
public class CastDataSO : ScriptableObject
{
    public CastType castType = CastType.NONE;
    public float minChargeTime;
    public float maxChargeTime;
    public int minDamage;
    public int maxDamage;
    public float chargeRate;
}