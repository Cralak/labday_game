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
        ChangeSettingsState();
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

    void ToggleSettings()
    {
        settingsCanvas.enabled = !settingsCanvas.enabled;

        // If the settings canvas is enabled, save the current state and adjust settings
        if (settingsCanvas.enabled)
        {
            cursorState = Cursor.lockState == CursorLockMode.Locked;
            UIState = UI.enabled;
            playerMovementState = playerMovement.enabled;
            diaryState  = diary.enabled;
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
            if (cursorState) Cursor.lockState = CursorLockMode.Locked;
            UI.enabled = UIState;
            playerMovement.enabled = playerMovementState;
            diary.enabled = diaryState;
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
