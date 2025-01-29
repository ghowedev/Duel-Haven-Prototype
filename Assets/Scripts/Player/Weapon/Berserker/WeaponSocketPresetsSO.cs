using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WeaponSocketData", menuName = "Game/Weapon Socket Data")]
public class WeaponSocketPresetsSO : ScriptableObject
{
    [System.Serializable]
    public class DirectionPreset
    {
        public string direction;
        public Vector3 position;
        public float rotation;
        public bool flipWeapon;
    }

    [SerializeField]
    private DirectionPreset[] directionPresets = new DirectionPreset[8];

    private Dictionary<Directions, DirectionPreset> presetDictionary;

    private void OnEnable()
    {
        FillArrayWithDirectionNames();
        InitializePresetDictionary();
    }

    private void InitializePresetDictionary()
    {
        presetDictionary = new Dictionary<Directions, DirectionPreset>();
        for (int i = 0; i < directionPresets.Length; i++)
        {
            presetDictionary.Add((Directions)i, directionPresets[i]);
        }
    }

    private void FillArrayWithDirectionNames()
    {
        for (int i = 0; i < directionPresets.Length; i++)
        {
            directionPresets[i].direction = ((Directions)i).ToString();
        }

    }

    public DirectionPreset GetPreset(Directions direction)
    {
        Debug.Log(presetDictionary[direction]);
        return presetDictionary[direction];
    }
}