using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundsManager : MonoBehaviour
{

    public Sound[] sounds;

    // Use this for initialization
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void OnPlayerConnected(string name)
    {
       Sound s =  Array.Find(sounds, sound => sound.name == name);
       s.source
    }
}
