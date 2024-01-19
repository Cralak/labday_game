using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
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

    void Awake()
    {
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        footsteps = player.GetComponent<AudioSource>();
        settingsCanvas = GameObject.Find("SettingsCanvas").GetComponent<Canvas>();
        UI = GameObject.Find("UI").GetComponent<Canvas>();
        settingsCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingsCanvas.enabled = !settingsCanvas.enabled;
            if (settingsCanvas.enabled)
            {
                cursorState = Cursor.lockState == CursorLockMode.Locked;
                UIState = UI.enabled;
                playerMovementState = playerMovement.enabled;

                Cursor.lockState = CursorLockMode.None;
                UI.enabled = false;
                playerMovement.enabled = false;
                footsteps.Pause();
            }
            else
            {
                if (cursorState) Cursor.lockState = CursorLockMode.Locked;
                UI.enabled = UIState;
                playerMovement.enabled = playerMovementState;
            }
        }
    }
}
