using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSocket : MonoBehaviour
{
    [SerializeField]
    private WeaponSocketPresetsSO presets;
    [SerializeField]
    private GameObject Weapon;


    public void UpdateSocketPosition(int direction)
    {
        WeaponSocketPresetsSO.DirectionPreset preset = presets.GetPreset(direction);
        transform.localPosition = preset.position;
        Weapon.transform.localRotation = Quaternion.Euler(0, 0, preset.rotation);
    }
}
