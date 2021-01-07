using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        foreach(Sound s in sounds)
        {
            s.SetSource(gameObject.AddComponent<AudioSource>());
            s.GetSource().clip = s.GetClip();
            s.GetSource().volume = s.GetVolume();
            s.GetSource().pitch = s.GetPitch();
            s.GetSource().loop = s.GetLoop();
        }
    }
    private void Start()
    {
        Play("Background");
    }

    public void Play(string name)
    {
        Sound s =  Array.Find(sounds, sound => sound.GetName() == name);
        if (s!=null)
        {
            s.GetSource().Play();
        }
       
    }
}
