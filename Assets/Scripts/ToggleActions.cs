using UnityEngine;

public class ToggleActions : MonoBehaviour
{
    static public bool IsPressed(string key)
    {
        // Detect if key is pressed 
        switch (PlayerPrefs.GetString(key))
        {
            case "escape":
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    return true;
                }
                break;
            case "tab":
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    return true;
                }
                break;
            case "lock":
                if (Input.GetKeyDown(KeyCode.CapsLock))
                {
                    return true;
                }
                break;
            case "backspace":
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    return true;
                }
                break;
            case "return":
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    return true;
                }
                break;
            case "space":
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    return true;
                }
                break;
            case "shift":
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    return true;
                }
                break;
            case "alt":
                if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
                {
                    return true;
                }
                break;
            case "control":
                if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                {
                    return true;
                }
                break;
            case "meta":
                if (Input.GetKeyDown(KeyCode.LeftMeta) || Input.GetKeyDown(KeyCode.RightMeta))
                {
                    return true;
                }
                break;
            case "upArrow":
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    return true;
                }
                break;
            case "downArrow":
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    return true;
                }
                break;
            case "leftArrow":
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    return true;
                }
                break;
            case "rightArrow":
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    return true;
                }
                break;
            case "leftClick":
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    return true;
                }
                break;
            case "rightClick":
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    return true;
                }
                break;
            case "wheelClick":
                if (Input.GetKeyDown(KeyCode.Mouse2))
                {
                    return true;
                }
                break;
            default:
                if (Input.GetKeyDown(PlayerPrefs.GetString(key)))
                {
                    return true;
                }
                break;
        }
        return false;
    }

    static public bool IsHeld(string key)
    {
        // Detect if key is pressed 
        switch (PlayerPrefs.GetString(key))
        {
            case "escape":
                if (Input.GetKey(KeyCode.Escape))
                {
                    return true;
                }
                break;
            case "tab":
                if (Input.GetKey(KeyCode.Tab))
                {
                    return true;
                }
                break;
            case "lock":
                if (Input.GetKey(KeyCode.CapsLock))
                {
                    return true;
                }
                break;
            case "backspace":
                if (Input.GetKey(KeyCode.Backspace))
                {
                    return true;
                }
                break;
            case "return":
                if (Input.GetKey(KeyCode.Return))
                {
                    return true;
                }
                break;
            case "space":
                if (Input.GetKey(KeyCode.Space))
                {
                    return true;
                }
                break;
            case "shift":
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    return true;
                }
                break;
            case "alt":
                if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
                {
                    return true;
                }
                break;
            case "control":
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    return true;
                }
                break;
            case "meta":
                if (Input.GetKey(KeyCode.LeftMeta) || Input.GetKey(KeyCode.RightMeta))
                {
                    return true;
                }
                break;
            case "upArrow":
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    return true;
                }
                break;
            case "downArrow":
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    return true;
                }
                break;
            case "leftArrow":
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    return true;
                }
                break;
            case "rightArrow":
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    return true;
                }
                break;
            case "leftClick":
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    return true;
                }
                break;
            case "rightClick":
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    return true;
                }
                break;
            case "wheelClick":
                if (Input.GetKey(KeyCode.Mouse2))
                {
                    return true;
                }
                break;
            default:
                if (Input.GetKey(PlayerPrefs.GetString(key)))
                {
                    return true;
                }
                break;
        }
        return false;
    }

    static public bool IsUnpressed(string key)
    {
        // Toggle the inventory on/off with the inventory key
        switch (PlayerPrefs.GetString(key))
        {
            case "escape":
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    return true;
                }
                break;
            case "tab":
                if (Input.GetKeyUp(KeyCode.Tab))
                {
                    return true;
                }
                break;
            case "lock":
                if (Input.GetKeyUp(KeyCode.CapsLock))
                {
                    return true;
                }
                break;
            case "backspace":
                if (Input.GetKeyUp(KeyCode.Backspace))
                {
                    return true;
                }
                break;
            case "return":
                if (Input.GetKeyUp(KeyCode.Return))
                {
                    return true;
                }
                break;
            case "space":
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    return true;
                }
                break;
            case "shift":
                if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
                {
                    return true;
                }
                break;
            case "alt":
                if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt))
                {
                    return true;
                }
                break;
            case "control":
                if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
                {
                    return true;
                }
                break;
            case "meta":
                if (Input.GetKeyUp(KeyCode.LeftMeta) || Input.GetKeyUp(KeyCode.RightMeta))
                {
                    return true;
                }
                break;
            case "upArrow":
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    return true;
                }
                break;
            case "UpArrow":
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    return true;
                }
                break;
            case "leftArrow":
                if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    return true;
                }
                break;
            case "rightArrow":
                if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    return true;
                }
                break;
            case "leftClick":
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    return true;
                }
                break;
            case "rightClick":
                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    return true;
                }
                break;
            case "wheelClick":
                if (Input.GetKeyUp(KeyCode.Mouse2))
                {
                    return true;
                }
                break;
            default:
                if (Input.GetKeyUp(PlayerPrefs.GetString(key)))
                {
                    return true;
                }
                break;
        }
        return false;
    }
}
