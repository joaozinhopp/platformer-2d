using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    public AudioClip ambienceClip; // Assign this in the Inspector
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = ambienceClip;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = 0.5f; // Adjust the volume as needed
    }

    private void Start()
    {
        PlayAmbience();
    }

    public void PlayAmbience()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopAmbience()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void ChangeAmbienceClip(AudioClip newClip)
    {
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();
    }
}
