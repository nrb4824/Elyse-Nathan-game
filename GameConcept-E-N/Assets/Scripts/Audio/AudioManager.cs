using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;

    public AudioSource[] gullSounds;

    [SerializeField] AudioMixer mixer;

    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

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

        LoadVolume();
    }

    private void LoadVolume() // Volume saved in VolumeSettings.cs
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume * 20));
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume * 20));
    }

    public void PlayGullSounds()
    {
        StartCoroutine(PlayGullCoRoutine());
    }

    IEnumerator PlayGullCoRoutine()
    {
        var waitTime = UnityEngine.Random.Range(8.0f, 20.0f);
        var birdCount = UnityEngine.Random.Range(0, 8);
        gullSounds[birdCount].Play();
        yield return new WaitForSeconds(waitTime);
        PlayGullSounds();
    }

    public void StopGullSounds()
    {
        StopCoroutine(PlayGullCoRoutine());
    }


    //Finds the name of the sound in the array and plays it
    //If the sound isn't in the array throws a warning.
    public void Play(string name)
    {
        
        AudioSource s = Array.Find(audioSources, audioSources => audioSources.name == name);
        if (name=="Walking")
        {
            s.loop = true;
        }
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.Play();
    }

    public void Stop(string name)
    {
        AudioSource s = Array.Find(audioSources, audioSources => audioSources.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.Stop();
    }
}
