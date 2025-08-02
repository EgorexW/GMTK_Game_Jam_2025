using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager i;

    public AudioMixerGroup mixerGroup;

    [InlineEditor] public Sound[] sounds;

    readonly List<AudioSource> audioSources = new();

    void Awake()
    {
        if (i != null){
            Destroy(gameObject);
        }
        else{
            i = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (var s in sounds){
            var source = gameObject.AddComponent<AudioSource>();
            audioSources.Add(source);
            source.loop = s.loop;

            source.outputAudioMixerGroup = mixerGroup;
        }
    }

    public void Play(string sound)
    {
        var nr = Array.FindIndex(sounds, item => item.name == sound);
        if (nr == -1){
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        var s = sounds[nr];
        var source = audioSources[nr];
        if (source.isPlaying && !s.canOverride){
            return;
        }
        source.clip = s.GetClip();
        source.volume = s.volume * (1f + Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        source.pitch = s.pitch * (1f + Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        source.Play();
    }

    public void Stop(string sound)
    {
        var nr = Array.FindIndex(sounds, item => item.name == sound);
        if (nr == -1){
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        var s = sounds[nr];
        var source = audioSources[nr];
        source.Stop();
    }
}