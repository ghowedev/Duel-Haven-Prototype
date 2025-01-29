using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSocket : MonoBehaviour
{
    [SerializeField]
    private WeaponSocketPresetsSO presets;

    public void UpdateSocketPosition(Directions direction)
    {
        WeaponSocketPresetsSO.DirectionPreset preset = presets.GetPreset(direction);
        transform.localPosition = preset.position;
        transform.localRotation = Quaternion.Euler(0, 0, preset.rotation);
    }
}
