using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HospitalEnter : MonoBehaviour
{
    [SerializeField] GameObject key; // Reference to the key GameObject

    GameObject player;
    PlayerMovement playerMovement;
    Diary diary;
    bool isTouching; // Flag to check if the player is touching the trigger area
    bool firstTry; // To check if player already tried to enter
    Canvas text;
    Inventory inventoryScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        inventoryScript = GameObject.Find("Inventory").GetComponent<Inventory>();
        isTouching = false;
        firstTry = true;
        text = GetComponent<Canvas>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is touching the trigger area, and presses the "e" key
        if (isTouching) PressToEnter();
    }

    // Called when another collider enters the trigger area
    void OnTriggerEnter()
    {
        isTouching = true;
        text.enabled = true;
    }

    // Called when another collider exits the trigger area
    void OnTriggerExit()
    {
        isTouching = false;
        text.enabled = false;
    }

    // Coroutine to load the hospital scene
    IEnumerator LoadHospital()
    {
        playerMovement.enabled = false;
        player.transform.position = new Vector3(4.0f, 1.0f, 2.0f);
        inventoryScript.inventory.Remove(key);

        yield return new WaitForSeconds(0.1f);

        diary.events.Add("indoor");
        playerMovement.enabled = true;
        SceneManager.LoadScene("IndoorScene");
    }

    void Enter()
    {
        // Check if the player has the key in inventory
        if (inventoryScript.inventory.Contains(key))
        {
            StartCoroutine(LoadHospital());
        }
        else if (firstTry)
        {
            diary.events.Add("doorLock");
            firstTry = false;
        }
    }

    void PressToEnter()
    {
        // Toggle the inventory on/off with the inventory key
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
