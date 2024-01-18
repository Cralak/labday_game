using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetSFXVolume : MonoBehaviour
{
    Slider slider;
    TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SFX")) PlayerPrefs.SetFloat("SFX", 1);

        slider = GetComponentInChildren<Slider>();
        inputField = GetComponentInChildren<TMP_InputField>();

        slider.value = PlayerPrefs.GetFloat("SFX");
        inputField.text = PlayerPrefs.GetFloat("SFX").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetFloat("SFX") != slider.value)
        {
            PlayerPrefs.SetFloat("SFX", slider.value);
            inputField.text = PlayerPrefs.GetFloat("SFX").ToString();
        }
        else if (PlayerPrefs.GetFloat("SFX").ToString() != inputField.text)
        {
            PlayerPrefs.SetFloat("SFX", float.Parse(inputField.text));
            slider.value = PlayerPrefs.GetFloat("SFX");
        }
    }
}
