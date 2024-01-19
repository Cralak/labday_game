using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSFXVolume : MonoBehaviour
{
    AudioSource forestSound;

    // Start is called before the first frame update
    void Start()
    {
        forestSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        forestSound.volume = PlayerPrefs.GetFloat("SFX");
    }
}
