using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    private float volume;
    GlobalDataHolder globalDataHolder;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        try
        {
            globalDataHolder = GameObject.Find("GlobalDataHolder").GetComponent<GlobalDataHolder>();
        }
        catch
        {

        }
        foreach (Sound s in sounds)
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
        ChangeVolume();
        StartCoroutine(StartFade("Background", 3f, 0.3f * globalDataHolder.RetrieveAndSaveSliderValue()));
    }

    public void ChangeVolume()
    {
        foreach (Sound s in sounds)
        {
            s.SetVolume(s.GetVolume() * globalDataHolder.RetrieveAndSaveSliderValue());
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.GetName() == name);
        if (s != null)
        {
            s.GetSource().Play();
        }
    }

    private AudioSource FindAudioSource(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.GetName() == name);
        return s.GetSource();
    }

    public IEnumerator StartFade(string name, float duration, float targetVolume)
    {
        AudioSource audioSource = FindAudioSource(name);
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}

