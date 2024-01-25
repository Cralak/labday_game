using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSFXVolume : MonoBehaviour
{
    Slider slider; // Reference to the Slider component
    TMP_InputField inputField; // Reference to the TMP_InputField component

    // Awake is called before Start and can be used for initialization
    void Awake()
    {
        // Check if the "SFX" key is present in PlayerPrefs, if not, set it to a default value of 1
        if (!PlayerPrefs.HasKey("SFX")) PlayerPrefs.SetFloat("SFX", 1);

        // Get references to the Slider and TMP_InputField components within the children of this GameObject
        slider = GetComponentInChildren<Slider>();
        inputField = GetComponentInChildren<TMP_InputField>();

        // Initialize the slider and input field values based on the saved SFX volume value in PlayerPrefs
        slider.value = PlayerPrefs.GetFloat("SFX");
        inputField.text = PlayerPrefs.GetFloat("SFX").ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the SFX volume value in PlayerPrefs does not match the slider value
        if (PlayerPrefs.GetFloat("SFX") != slider.value)
        {
            // Update PlayerPrefs with the new SFX volume value based on the slider, and update the input field text
            PlayerPrefs.SetFloat("SFX", slider.value);
            inputField.text = PlayerPrefs.GetFloat("SFX").ToString();
        }
        // Check if the SFX volume value in PlayerPrefs does not match the text in the input field
        else if (PlayerPrefs.GetFloat("SFX").ToString() != inputField.text)
        {
            // Update PlayerPrefs with the new SFX volume value based on the input field, and update the slider value
            PlayerPrefs.SetFloat("SFX", float.Parse(inputField.text));
            slider.value = PlayerPrefs.GetFloat("SFX");
        }
    }
}
