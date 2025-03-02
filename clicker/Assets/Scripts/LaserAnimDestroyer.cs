using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAnimDestroyer : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip laserSoundClip;
    private AudioSource mAudioSource;

    void Awake()
    {
        mAudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayLaserSound();
    }

    private void PlayLaserSound()
    {
        //Reproducimos sonido de Disparo
        mAudioSource.PlayOneShot(laserSoundClip, 0.70f);
    }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
}
