using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Ability", menuName = "Ability/DamageData")]

public class DamageDataSO : ScriptableObject
{
    public float damage;
}