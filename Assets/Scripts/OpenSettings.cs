using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSettings : MonoBehaviour
{
    Canvas settingsCanvas;

    void Awake()
    {
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    // Start is called before the first frame update
    void Start()
    {
        settingsCanvas = GameObject.Find("SettingsCanvas").GetComponent<Canvas>();
        settingsCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingsCanvas.enabled = !settingsCanvas.enabled;
            Cursor.lockState = settingsCanvas.enabled == true ? CursorLockMode.None : CursorLockMode.Locked;;
        }
    }
}
