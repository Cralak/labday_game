using UnityEngine;

public class SetSFXVolume : MonoBehaviour
{
    AudioSource forestSound;

    void Start()
    {
        InitializeComponents();
    }

    void Update()
    {
        UpdateSFXVolume();
    }

    void InitializeComponents()
    {
        // Initialize required components
        forestSound = GetComponent<AudioSource>();
    }

    void UpdateSFXVolume()
    {
        // Update SFX volume based on player preferences
        forestSound.volume = PlayerPrefs.GetFloat("SFX");
    }
}

