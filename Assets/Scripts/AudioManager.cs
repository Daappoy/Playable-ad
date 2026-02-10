using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //audioSource
    public AudioSource audioSource;
    //audio clips
    public AudioClip jumpSound;
    public AudioClip shootSound;
    public AudioClip hurtSound;
    public AudioClip powerUpSound;
    public AudioClip coinSound;

    public static AudioManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
