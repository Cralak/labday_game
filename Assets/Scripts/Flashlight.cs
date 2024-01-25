using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light lightComponent; // Reference to the Light component

    // Update is called once per frame
    void Update()
    {
        ToggleFlashlight();
    }

    void ToggleFlashlight()
    {
        // Toggle the flashlight on/off with the flashlight key
        switch (PlayerPrefs.GetString("flashlight"))
        {
            case "escape":
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "tab":
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "lock":
                if (Input.GetKeyDown(KeyCode.CapsLock))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "backspace":
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "return":
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "space":
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "shift":
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "alt":
                if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "control":
                if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "meta":
                if (Input.GetKeyDown(KeyCode.LeftMeta) || Input.GetKeyDown(KeyCode.RightMeta))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "upArrow":
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "downArrow":
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "leftArrow":
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "rightArrow":
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "leftClick":
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "rightClick":
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            case "wheelClick":
                if (Input.GetKeyDown(KeyCode.Mouse2))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
            default:
                if (Input.GetKeyDown(PlayerPrefs.GetString("flashlight")))
                {
                    lightComponent.enabled = !lightComponent.enabled;
                }
                break;
        }
    }
}

