using UnityEngine;

public class SetSFXVolume : MonoBehaviour
{
    AudioSource forestSound;

    void Start()
    {
        // Initialize required components
        forestSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Update SFX volume based on player preferences
        forestSound.volume = PlayerPrefs.GetFloat("SFX");
    }
}

