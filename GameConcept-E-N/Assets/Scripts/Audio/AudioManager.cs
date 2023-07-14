using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public bool birds;
    public Sound[] birdSounds;
    public float birdVolume;

    public static AudioManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound b in birdSounds)
        {
            b.source = gameObject.AddComponent<AudioSource>();
            b.source.clip = b.clip;
            b.source.volume = birdVolume;
            b.source.pitch = b.pitch;
            b.source.loop = b.loop;
        }
    }
    public void PlayBirds()
    {
        StartCoroutine(Birds());
    }
    private IEnumerator Birds()
    {
        var index = UnityEngine.Random.Range(0, 44);
        var index2 = UnityEngine.Random.Range(0, 44);
        var index3 = UnityEngine.Random.Range(0, 44);
        var wait = UnityEngine.Random.Range(0, 1.0f);
        var wait2 = UnityEngine.Random.Range(0, 1.0f);
        var wait3 = UnityEngine.Random.Range(0, 1.0f);
        Sound b = birdSounds[index];
        Sound b2 = birdSounds[index2];
        Sound b3 = birdSounds[index3];
        b.source.Play();
        yield return new WaitForSeconds(wait);
        b2.source.Play();
        yield return new WaitForSeconds(wait2);
        b3.source.Play();
        yield return new WaitForSeconds(wait3);
        PlayBirds();
    }

    //Finds the name of the sound in the array and plays it
    //If the sound isn't in the array throws a warning.
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
}
