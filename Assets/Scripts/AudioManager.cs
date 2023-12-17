using System;
using UnityEngine;

//script for managing audio in game
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    //load all sounds to sound manager, for more easily management
    private void Awake()
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
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        PlayOnGameStart();
    }

    //play specific sound
    public void Play(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " does not exist!");
            return;
        }

        s.source.Play();
    }

    //stop playing specific sound
    public void Stop(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " does not exist!");
            return;
        }

        s.source.Stop();
    }

    //stop playing all sounds
    public void StopAll()
    {
        foreach (var s in sounds) 
            s.source.Stop();
    }

    //start playing these sounds when Scene Town loads
    public void PlayOnStart()
    {
        StopAll();
        Play("City");
        FadeIn("Background", 0.5f, 0.3f);
    }

    //start playing these sounds when Game loads - Scene with Menu
    public void PlayOnGameStart()
    {
        StopAll();
        Play("Background");
    }


    //used for FadeIn specific sounds
    public void FadeIn(string name, float duration, float targetVolume)
    {
        var s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " does not exist!");
            return;
        }

        s.source.Play();
        StartCoroutine(FadeAudioSource.StartFade(s.source, duration, targetVolume, true));
    }

    //used for FadeIn specific sounds - previously used with "cracking" sounds, which are commented
    public void FadeOut(string name, float duration, float targetVolume)
    {
        var s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " does not exist!");
            return;
        }

        StartCoroutine(FadeAudioSource.StartFade(s.source, duration, targetVolume, false));
        //s.source.Stop();
    }
}