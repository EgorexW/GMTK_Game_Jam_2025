using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public sealed class PlayAudio : MonoBehaviour
{
    [GetComponent][SerializeField] AudioSource audioSource;
    
    [Required][SerializeField][InlineEditor] Sound sound;
    
    [SerializeField] bool playOnStart = false;
    
    void Start()
    {
        if (playOnStart){
            Play();
        }
    }
    
    void Reset(){
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    [Button]
    public void Play(){
        if (audioSource.isPlaying && !sound.canOverride){
            return;
        }
        audioSource.loop = sound.loop;
		audioSource.outputAudioMixerGroup = sound.mixerGroup;
        audioSource.clip = sound.GetClip();
        audioSource.volume = sound.volume * (1f + UnityEngine.Random.Range(-sound.volumeVariance / 2f, sound.volumeVariance / 2f));
		audioSource.pitch = sound.pitch * (1f + UnityEngine.Random.Range(-sound.pitchVariance / 2f, sound.pitchVariance / 2f));
        audioSource.Play();
    }
}
