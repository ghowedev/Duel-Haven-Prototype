using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WeaponSocketData", menuName = "Game/Weapon Socket Data")]
public class WeaponSocketPresetsSO : ScriptableObject
{
    [System.Serializable]
    public class DirectionPreset
    {
        public int direction;
        public Vector3 position;
        public float rotation;
        public bool flipWeapon;
    }

    [SerializeField]
    private DirectionPreset[] directionPresets = new DirectionPreset[8];

    private Dictionary<int, DirectionPreset> presetDictionary;

    private void OnEnable()
    {
        // FillArrayWithDirectionNames();
        InitializePresetDictionary();
    }

    /*
        private void InitializePresetDictionary()
        {
            presetDictionary = new Dictionary<int, DirectionPreset>();
            for (int i = 0; i < directionPresets.Length; i++)
            {
                presetDictionary.Add((int)i, directionPresets[i]);
            }
        }
    */

    private void InitializePresetDictionary()
    {
        presetDictionary = new Dictionary<int, DirectionPreset>();

        foreach (var preset in directionPresets)
        {
            if (preset != null)
                presetDictionary[preset.direction] = preset;
        }
    }

    private void FillArrayWithDirectionNames()
    {
        for (int i = 0; i < directionPresets.Length; i++)
        {
            directionPresets[i].direction = ((int)i);
        }

    }

    public DirectionPreset GetPreset(int direction)
    {
        return presetDictionary[direction];
    }
}