using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ArchEntrance : MonoBehaviour
{
    [SerializeField] Image blackScreen; // Reference to the Image component for the black screen effect
    [SerializeField] Canvas canvas; // Reference to the Canvas component

    GameObject player; // Reference to the player GameObject
    Diary diary; // Reference to the Diary script
    Canvas UI; // Reference to the UI Canvas
    bool isTouching; // Flag to check if the player is touching the trigger area
    PlayerMovement playerMovement; // Reference to the PlayerMovement script
    Canvas text; // Reference to another Canvas component (possibly for displaying text)

    // Start is called before the first frame update
    void Start()
    {
        // Find and assign references to necessary components and objects
        player = GameObject.Find("Player");
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        UI = GameObject.Find("UI").GetComponent<Canvas>();
        playerMovement = player.GetComponent<PlayerMovement>();
        blackScreen = canvas.GetComponentInChildren<Image>();
        text = GetComponent<Canvas>();
        isTouching = false;
        text.enabled = false;

        // Set the initial alpha value of the black screen to 0
        Color c = blackScreen.color;
        c.a = 0;
        blackScreen.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is touching and presses the interact key
        if (isTouching) PressToEnter();
    }

    // Called when another Collider enters the trigger zone
    void OnTriggerEnter()
    {
        // Set the touching flag to true and enable the associated text Canvas
        isTouching = true;
        text.enabled = true;
    }

    // Called when another Collider exits the trigger zone
    void OnTriggerExit()
    {
        // Set the touching flag to false and disable the associated text Canvas
        isTouching = false;
        text.enabled = false;
    }

    // Coroutine for the fake screen effect
    IEnumerator FakeScreen()
    {
        // Fade in the black screen
        for (float i = 0; i <= 1; i += 0.05f)
        {
            Color c = blackScreen.color;
            c.a = i;
            blackScreen.color = c;

            // Wait for a short duration before the next iteration
            yield return new WaitForSeconds(0.005f);
        }

        // Move the player to a specific position
        player.transform.position = new Vector3(22, 1, 50);

        // Fade out the black screen
        for (float i = 1.0f; i >= 0; i -= 0.05f)
        {
            Color c = blackScreen.color;
            c.a = i;
            blackScreen.color = c;

            // Wait for a short duration before the next iteration
            yield return new WaitForSeconds(0.005f);
        }

        // Add an event to the diary, and enable player movement and UI
        diary.events.Add("archEnter");
        playerMovement.enabled = true;
        UI.enabled = true;
    }

    void Enter()
    {
        // Disable player movement, pause player audio, and hide UI
        playerMovement.enabled = false;
        player.GetComponent<AudioSource>().Pause();
        UI.enabled = false;

        // Start the coroutine for the fake screen effect
        StartCoroutine(FakeScreen());

        // Reset the touching flag
        isTouching = false;
    }

    void PressToEnter()
    {
        // Enter the if interact button is pressed
        switch (PlayerPrefs.GetString("interact"))
        {
            case "escape":
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Enter();
                }
                break;
            case "tab":
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    Enter();
                }
                break;
            case "lock":
                if (Input.GetKeyDown(KeyCode.CapsLock))
                {
                    Enter();
                }
                break;
            case "backspace":
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    Enter();
                }
                break;
            case "return":
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Enter();
                }
                break;
            case "space":
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Enter();
                }
                break;
            case "shift":
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    Enter();
                }
                break;
            case "alt":
                if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
                {
                    Enter();
                }
                break;
            case "control":
                if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                {
                    Enter();
                }
                break;
            case "meta":
                if (Input.GetKeyDown(KeyCode.LeftMeta) || Input.GetKeyDown(KeyCode.RightMeta))
                {
                    Enter();
                }
                break;
            case "upArrow":
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Enter();
                }
                break;
            case "downArrow":
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Enter();
                }
                break;
            case "leftArrow":
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Enter();
                }
                break;
            case "rightArrow":
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Enter();
                }
                break;
            case "leftClick":
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Enter();
                }
                break;
            case "rightClick":
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Enter();
                }
                break;
            case "wheelClick":
                if (Input.GetKeyDown(KeyCode.Mouse2))
                {
                    Enter();
                }
                break;
            default:
                if (Input.GetKeyDown(PlayerPrefs.GetString("interact")))
                {
                    Enter();
                }
                break;
        }
    }
}