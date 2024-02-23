using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    readonly List<GameObject> sections = new();

    GameObject player;
    PlayerMovement playerMovement;
    Diary diary;
    Inventory inventory;
    AudioSource footsteps;
    Canvas UI;
    Canvas settingsCanvas;
    bool cursorState;
    bool UIState;
    bool playerMovementState;
    bool inventoryState;
    bool diaryState;

    // Start is called before the first frame update
    void Start()
    {
        // Find and assign necessary GameObjects and components
        sections.Add(GameObject.Find("General Section"));
        sections.Add(GameObject.Find("Controls Section"));
        ChangeSection("General Section");

        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        footsteps = player.GetComponent<AudioSource>();
        settingsCanvas = GetComponent<Canvas>();
        UI = GameObject.Find("UI").GetComponent<Canvas>();
        settingsCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle the settings canvas on/off when the settings key is pressed
        if (ToggleActions.IsPressed("settings")) ToggleSettings();
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

    public void ToggleSettings()
    {
        settingsCanvas.enabled = !settingsCanvas.enabled;

        // If the settings canvas is enabled, save the current state and adjust settings
        if (settingsCanvas.enabled)
        {
            cursorState = Cursor.lockState == CursorLockMode.Locked;
            UIState = UI.enabled;
            playerMovementState = playerMovement.enabled;
            diaryState = diary.enabled;
            inventoryState = inventory.enabled;

            Cursor.lockState = CursorLockMode.None;
            UI.enabled = false;
            playerMovement.enabled = false;
            diary.enabled = false;
            inventory.enabled = false;
            footsteps.Pause();
        }
        // If the settings canvas is disabled, restore the previous state
        else
        {
            Cursor.lockState = cursorState ? CursorLockMode.Locked : CursorLockMode.None;
            UI.enabled = UIState;
            playerMovement.enabled = playerMovementState;
            diary.enabled = diaryState;
            inventory.enabled = inventoryState;
        }
    }
}
