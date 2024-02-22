using UnityEngine;

public class SetSFXVolume : MonoBehaviour
{
    AudioSource source;

    void Start()
    {
        // Initialize required components
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Update SFX volume based on player preferences
        source.volume = PlayerPrefs.GetFloat("SFX");
    }
}

