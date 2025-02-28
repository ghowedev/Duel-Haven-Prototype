using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Sound/SoundData")]
public class SoundDataSO : ScriptableObject
{
    public AudioClip soundClip;
    public float volume = 1f;
    public bool spatialized;
}
