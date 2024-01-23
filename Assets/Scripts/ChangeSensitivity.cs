using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSensitivity : MonoBehaviour
{
    Slider slider; // Reference to the Slider component
    TMP_InputField inputField; // Reference to the TMP_InputField component

    // Awake is called before Start and can be used for initialization
    void Awake()
    {
        // Check if the "sensitivity" key is present in PlayerPrefs, if not, set it to a default value of 5.0f
        if (!PlayerPrefs.HasKey("sensitivity"))
            PlayerPrefs.SetFloat("sensitivity", 5.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get references to the Slider and TMP_InputField components within the children of this GameObject
        slider = GetComponentInChildren<Slider>();
        inputField = GetComponentInChildren<TMP_InputField>();

        // Initialize the slider and input field values based on the saved sensitivity value in PlayerPrefs
        slider.value = (PlayerPrefs.GetFloat("sensitivity") - 1.0f) / 9.0f;
        inputField.text = PlayerPrefs.GetFloat("sensitivity").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the sensitivity value in PlayerPrefs does not match the calculated value from the slider
        if (Math.Round(PlayerPrefs.GetFloat("sensitivity"), 5) != Math.Round(slider.value * 9.0f + 1.0f, 5))
        {
            // Update PlayerPrefs with the new sensitivity value based on the slider, and update the input field text
            PlayerPrefs.SetFloat("sensitivity", slider.value * 9.0f + 1.0f);
            inputField.text = PlayerPrefs.GetFloat("sensitivity").ToString();
        }
        // Check if the sensitivity value in PlayerPrefs does not match the text in the input field
        else if (PlayerPrefs.GetFloat("sensitivity").ToString() != inputField.text)
        {
            // Update PlayerPrefs with the new sensitivity value based on the input field, and update the slider value
            PlayerPrefs.SetFloat("sensitivity", float.Parse(inputField.text));
            slider.value = (PlayerPrefs.GetFloat("sensitivity") - 1.0f) / 9.0f;
        }
    }
}