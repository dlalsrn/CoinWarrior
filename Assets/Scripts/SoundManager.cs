using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    private AudioSource audioSource;
    
    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(AudioClip sound, float volume) {
        audioSource.PlayOneShot(sound, volume);
    }
}
