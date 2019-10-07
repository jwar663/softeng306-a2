using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //initialise sound array
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
        }
    }
    //music played upon opening the scene
    private void Start()
    {
        Play("Theme");
    }

    //play sounds in different scripts
    public void Play(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        //show warning if sound is not found
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found.");
            return;
        }
        s.source.Play();
    }

    //stop a sound that is currently playing.
    public void Stop(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        //show warning if sound is not found
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found.");
            return;
        }
        s.source.Stop();
    }
}

