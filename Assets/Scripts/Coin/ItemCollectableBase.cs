using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particleSystem;
    public AudioClip collectSound; // Use AudioClip instead of AudioSource

    private void Awake()
    {
        if (particleSystem != null) particleSystem.transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        Debug.Log("Collect");
        OnCollect();
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        if (particleSystem != null) particleSystem.Play();
        PlayCollectSound();
    }

    private void PlayCollectSound()
    {
        if (collectSound != null)
        {
            // Create a new GameObject for playing the sound
            GameObject soundGameObject = new GameObject("CollectSound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = collectSound;
            audioSource.Play();
            // Destroy the GameObject after the sound has finished playing
            Destroy(soundGameObject, collectSound.length);
        }
        else
        {
            Debug.LogWarning("Collect sound is not assigned.");
        }
    }
}
