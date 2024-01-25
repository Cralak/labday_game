using TMPro;
using UnityEngine;

public class KeyChange : MonoBehaviour
{
    readonly string[] actions = {"forward", "backward", "left", "right", "jump", "crouch", "settings", "inventory", "diary", "flashlight", "interact"};

    bool pushed;
    string keyPressed;
    string move;
    bool isAvailable;
    TMP_Text text;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("forward"))
        {
            PlayerPrefs.SetString("forward", "z");
            PlayerPrefs.SetString("backward", "s");
            PlayerPrefs.SetString("left", "q");
            PlayerPrefs.SetString("right", "d");
            PlayerPrefs.SetString("jump", "space");
            PlayerPrefs.SetString("crouch", "control");
            PlayerPrefs.SetString("settings", "escape");
            PlayerPrefs.SetString("inventory", "i");
            PlayerPrefs.SetString("diary", "tab");
            PlayerPrefs.SetString("flashlight", "f");
            PlayerPrefs.SetString("interact", "e");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        move = transform.parent.name.ToLower();
        text = GetComponentInChildren<TMP_Text>();
        pushed = false;
        isAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = PlayerPrefs.GetString(move);

        if (pushed && Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                keyPressed = "escape";
                pushed = false;
            }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                keyPressed = "tab";
                pushed = false;
            }
            else if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                keyPressed = "lock";
                pushed = false;
            }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                keyPressed = "backspace";
                pushed = false;
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                keyPressed = "return";
                pushed = false;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                keyPressed = "space";
                pushed = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                keyPressed = "shift";
                pushed = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
            {
                keyPressed = "alt";
                pushed = false;
            }
            else if (
                Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)
            )
            {
                keyPressed = "control";
                pushed = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftMeta) || Input.GetKeyDown(KeyCode.RightMeta))
            {
                keyPressed = "meta";
                pushed = false;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                keyPressed = "upArrow";
                pushed = false;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                keyPressed = "downArrow";
                pushed = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                keyPressed = "leftArrow";
                pushed = false;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                keyPressed = "rightArrow";
                pushed = false;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                keyPressed = "leftClick";
                pushed = false;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                keyPressed = "rightClick";
                pushed = false;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                keyPressed = "wheelClick";
                pushed = false;
            }
            else
            {
                keyPressed = Input.inputString;
                if (keyPressed.Length == 1)
                {
                    pushed = false;
                }
            }

            if (!pushed)
            {
                foreach (string action in actions)
                {
                    if (action != move && PlayerPrefs.GetString(action) == keyPressed)
                    {
                        isAvailable = false;
                        break;
                    }
                }

                if (isAvailable)
                {
                    PlayerPrefs.SetString(move, keyPressed);
                }
                else
                {
                    print("Key already used");
                    isAvailable = true;
                }
            }
        }
    }

    public void ChangeKey()
    {
        pushed = true;
    }
}
