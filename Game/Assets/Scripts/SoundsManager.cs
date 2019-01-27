using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundsManager : MonoBehaviour
{

    public Sound[] sounds;

    public static SoundsManager instance;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Crowd");
    }

    public void Play(string name)
    {
       Sound s =  Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
            return;
        }
        s.source.Play();
    }
}
