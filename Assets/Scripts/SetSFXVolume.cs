using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSFXVolume : MonoBehaviour
{
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        if (!PlayerPrefs.HasKey("SFX"))
        {
            PlayerPrefs.SetFloat("SFX", 1);
        }
        slider.value = PlayerPrefs.GetFloat("SFX");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("SFX", slider.value);
    }
}
