using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    //name of clip
    public string name;

    //actual clip object
    public AudioClip clip;

    //slider for volume
    [Range(0f, 1f)]
    public float volume;

    //slider for pitch
    [Range(.1f, 3f)]
    public float pitch;

    //whether sound should loop or not
    public bool loop;

    //whether the sound should be represented at 2d or 3d
    [Range(0f, 1f)]
    public float spatialBlend;

    [HideInInspector]
    public AudioSource source;
}
