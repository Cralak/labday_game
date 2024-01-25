using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettings : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;
    Inventory inventory;
    AudioSource footsteps;
    Canvas UI;
    Canvas settingsCanvas;
    bool cursorState;
    bool UIState;
    bool playerMovementState;
    bool inventoryState;

    // Start is called before the first frame update
    void Start()
    {
        // Find and assign necessary GameObjects and components
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        inventory = player.GetComponent<Inventory>();
        footsteps = player.GetComponent<AudioSource>();
        settingsCanvas = GameObject.Find("Settings").GetComponent<Canvas>();
        UI = GameObject.Find("UI").GetComponent<Canvas>();
        settingsCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle the settings canvas on/off when the settings key is pressed
        ChangeSettingsState();
    }

    void ToggleSettings()
    {
        settingsCanvas.enabled = !settingsCanvas.enabled;

        // If the settings canvas is enabled, save the current state and adjust settings
        if (settingsCanvas.enabled)
        {
            cursorState = Cursor.lockState == CursorLockMode.Locked;
            UIState = UI.enabled;
            playerMovementState = playerMovement.enabled;
            inventoryState = inventory.enabled;

            Cursor.lockState = CursorLockMode.None;
            UI.enabled = false;
            playerMovement.enabled = false;
            inventory.enabled = false;
            footsteps.Pause();
        }
        // If the settings canvas is disabled, restore the previous state
        else
        {
            if (cursorState) Cursor.lockState = CursorLockMode.Locked;
            UI.enabled = UIState;
            playerMovement.enabled = playerMovementState;
            inventory.enabled = inventoryState;
        }
    }

    void ChangeSettingsState()
    {
        // Toggle the settings on/off with the settings key
        switch (PlayerPrefs.GetString("settings"))
        {
            case "escape":
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ToggleSettings();
                }
                break;
            case "tab":
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    ToggleSettings();
                }
                break;
            case "lock":
                if (Input.GetKeyDown(KeyCode.CapsLock))
                {
                    ToggleSettings();
                }
                break;
            case "backspace":
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    ToggleSettings();
                }
                break;
            case "return":
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    ToggleSettings();
                }
                break;
            case "space":
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ToggleSettings();
                }
                break;
            case "shift":
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    ToggleSettings();
                }
                break;
            case "alt":
                if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
                {
                    ToggleSettings();
                }
                break;
            case "control":
                if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                {
                    ToggleSettings();
                }
                break;
            case "meta":
                if (Input.GetKeyDown(KeyCode.LeftMeta) || Input.GetKeyDown(KeyCode.RightMeta))
                {
                    ToggleSettings();
                }
                break;
            case "upArrow":
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ToggleSettings();
                }
                break;
            case "downArrow":
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ToggleSettings();
                }
                break;
            case "leftArrow":
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    ToggleSettings();
                }
                break;
            case "rightArrow":
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    ToggleSettings();
                }
                break;
            case "leftClick":
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    ToggleSettings();
                }
                break;
            case "rightClick":
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    ToggleSettings();
                }
                break;
            case "wheelClick":
                if (Input.GetKeyDown(KeyCode.Mouse2))
                {
                    ToggleSettings();
                }
                break;
            default:
                if (Input.GetKeyDown(PlayerPrefs.GetString("settings")))
                {
                    ToggleSettings();
                }
                break;
        }
    }
}
