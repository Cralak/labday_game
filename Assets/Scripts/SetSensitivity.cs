using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class setSensitivity : MonoBehaviour
{
    Slider slider;
    TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("sensitivity")) PlayerPrefs.SetFloat("sensitivity", 5f);

        slider = GetComponentInChildren<Slider>();
        inputField = GetComponentInChildren<TMP_InputField>();

        slider.value = (PlayerPrefs.GetFloat("sensitivity") - 1f) / 9f;
        inputField.text = PlayerPrefs.GetFloat("sensitivity").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Round(PlayerPrefs.GetFloat("sensitivity"), 5) != Math.Round(slider.value * 9f + 1f, 5))
        {
            PlayerPrefs.SetFloat("sensitivity", slider.value * 9f + 1f);
            inputField.text = PlayerPrefs.GetFloat("sensitivity").ToString();
        }
        else if (PlayerPrefs.GetFloat("sensitivity").ToString() != inputField.text)
        {
            PlayerPrefs.SetFloat("sensitivity", float.Parse(inputField.text));
            slider.value = (PlayerPrefs.GetFloat("sensitivity") - 1f) / 9f;
        }
    }
}
