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

    [Tooltip("Animation clips mapped to numpad directions (1-9, ignoring 5)")]
    public AnimationClip[] animationDirectionClips = new AnimationClip[10];

    [HideInInspector]
    public int[] animationDirectionHashes = new int[10];
    private float _animationClipLength;
    public float animationClipLength => _animationClipLength;
    public AudioClip audioClip;

    public float range;
    public float castTime;
    public DamageDataSO damageDataSO;
    public CastDataSO castData;
    public ProjectileDataSO projectileData;


    public Type GetAbilityType()
    {
        return Type.GetType(abilityType);
    }

    private void OnValidate()
    {
        if (animationDirectionClips[2] != null)
        {
            _animationClipLength = animationDirectionClips[2].length;
            // Debug.Log("Clip length is: " + animationClipLength + " seconds.");
        }
        GenerateAnimationHashes();


    }

    private void OnEnable()
    {
        // Fallback in case hashes weren't generated in editor
        bool needsGeneration = false;
        for (int i = 0; i < animationDirectionHashes.Length; i++)
        {
            if (animationDirectionClips[i] != null && animationDirectionHashes[i] == 0)
            {
                needsGeneration = true;
                break;
            }
        }

        if (needsGeneration)
        {
            GenerateAnimationHashes();
        }
    }

    private void GenerateAnimationHashes()
    {
        for (int i = 0; i < animationDirectionClips.Length; i++)
        {
            if (animationDirectionClips[i] != null)
            {
                animationDirectionHashes[i] = Animator.StringToHash(animationDirectionClips[i].name);
                // Debug.Log(animationDirectionClips[i].name + ": " + animationDirectionHashes[i]);
            }
        }
    }
}