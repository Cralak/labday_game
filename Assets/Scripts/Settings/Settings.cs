using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    readonly List<GameObject> sections = new();

    Canvas UI;
    Canvas settingsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        // Find and assign necessary GameObjects and components
        sections.Add(GameObject.Find("General Section"));
        sections.Add(GameObject.Find("Controls Section"));
        ChangeSection("General Section");

        settingsCanvas = GetComponent<Canvas>();
        UI = GameObject.Find("UI").GetComponent<Canvas>();
        settingsCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle the settings canvas on/off when the settings key is pressed
        if (ToggleActions.IsPressed("settings"))
        {
            if (!settingsCanvas.enabled)
            {
                if (!UIState.isBusy) ShowSettings();
            }
            else
            {
                HideSettings();
            }
        }
    }

    public void ShowSettings()
    {
        UIState.isBusy = true;
        ChangePlayerState.Disable();
        Cursor.lockState = CursorLockMode.None;
        UI.enabled = false;
        settingsCanvas.enabled = true;
    }

    void HideSettings()
    {
        UIState.isBusy = false;
        ChangePlayerState.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        UI.enabled = true;
        settingsCanvas.enabled = false;
    }

    public void ChangeSection(string name)
    {
        // Activate section with given name and desactive other ones
        foreach (GameObject section in sections)
        {
            if (section.name == name) section.SetActive(true);
            else section.SetActive(false);
        }
    }
}
