
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance{ get; private set;}
    private AudioSource source;
    public AudioClip runSound;


    private void Awake(){
        instance = this;
        source = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);

    }
    public void StopSound(AudioClip _sound)
    {
        source.clip = _sound;
        source.Stop();
    }
}
