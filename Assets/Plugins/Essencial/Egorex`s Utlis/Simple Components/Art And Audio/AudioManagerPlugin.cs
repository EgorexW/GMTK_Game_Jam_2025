using UnityEngine;

public class AudioManagerPlugin : MonoBehaviour
{
    [SerializeField] string soundName;

    public void Play(){
        AudioManager.i.Play(soundName);
    }
}
