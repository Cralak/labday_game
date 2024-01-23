using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSettings : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;
    AudioSource footsteps;
    Canvas UI;
    Canvas settingsCanvas;
    bool cursorState;
    bool UIState;
    bool playerMovementState;

    // Start is called before the first frame update
    void Start()
    {
        // Find necessary components and initialize settings
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        footsteps = player.GetComponent<AudioSource>();
        settingsCanvas = GameObject.Find("Settings").GetComponent<Canvas>();
        settingsCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle settings canvas visibility on 'Escape' key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingsCanvas.enabled = !settingsCanvas.enabled;
            if (settingsCanvas.enabled)
            {
                // Save current states before modifying
                cursorState = Cursor.lockState == CursorLockMode.Locked;
                UIState = UI.enabled;
                playerMovementState = playerMovement.enabled;

                // Modify states for settings
                Cursor.lockState = CursorLockMode.None;
                UI.enabled = false;
                playerMovement.enabled = false;
                footsteps.Pause();
            }
            else
            {
                // Restore states to previous values
                if (cursorState) Cursor.lockState = CursorLockMode.Locked;
                UI.enabled = UIState;
                playerMovement.enabled = playerMovementState;
            }
        }
    }
}
