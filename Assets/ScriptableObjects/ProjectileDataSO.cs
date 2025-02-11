using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Ability", menuName = "Ability/ProjectileData")]

public class ProjectileDataSO : ScriptableObject
{
    public float speed;
    public float priority;
    public GameObject prefab;

    public void Spawn()
    {
        Instantiate(prefab);
    }
}