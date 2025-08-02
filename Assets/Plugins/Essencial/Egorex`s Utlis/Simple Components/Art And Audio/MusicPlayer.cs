using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer i;

    void Awake()
    {
        if (i != null){
            Destroy(gameObject);
        }
        else{
            i = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        AudioManager.i.Play("Music");
    }
}