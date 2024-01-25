using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Chess : MonoBehaviour
{
    public bool isPlaying; // Flag to check if the chess puzzle is currently in progress

    // References to chess pieces GameObjects
    [SerializeField] GameObject pieceWhite1;
    [SerializeField] GameObject pieceWhite2;
    [SerializeField] GameObject pieceWhite3;
    [SerializeField] GameObject pieceBlack1;
    [SerializeField] GameObject pieceBlack2;
    [SerializeField] GameObject pieceBlack3;
    [SerializeField] GameObject pieceBlack4;

    // References to other game objects and components
    GameObject player;
    PlayerMovement playerMovement;
    Inventory inventory;
    AudioSource footsteps;
    GameObject playerCamera;
    Camera componentCamera;
    GameObject flashlight;
    Diary diary;
    Canvas UI;
    bool isTouching;
    bool isSwitching;
    GameObject square;
    string lastClicked;
    bool firstMoveDone;
    bool secondMoveDone;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize references to game objects and components
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        footsteps = player.GetComponent<AudioSource>();
        playerCamera = GameObject.Find("PlayerCamera");
        componentCamera = playerCamera.GetComponent<Camera>();
        flashlight = GameObject.Find("Flashlight");
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        UI = GameObject.Find("UI").GetComponent<Canvas>();
        isTouching = false;
        isPlaying = false;
        isSwitching = false;
        lastClicked = "64"; // Initial value to avoid null reference
    }

    // Update is called once per frame
    void Update()
    {
        // Check if chess puzzle is not in progress
        if (!isPlaying)
        {
            // Check if player is touching and not currently switching, and interact key is pressed
            if (isTouching && !isSwitching && HasInteracted()) StartCoroutine(Play()); // Start chess puzzle
        }
        else
        {
            StartCoroutine(Puzzle()); // Continue puzzle logic

            // Check if player is touching and not currently switching, and interact key is pressed
            if (isTouching && !isSwitching && HasInteracted()) StartCoroutine(Unplay()); // End chess puzzle
        }
    }

    // Triggered when another collider enters the trigger zone
    void OnTriggerEnter(Collider collider)
    {
        if (collider == player.GetComponent<CharacterController>()) isTouching = true;
    }

    // Triggered when another collider exits the trigger zone
    void OnTriggerExit(Collider collider)
    {
        if (collider == player.GetComponent<CharacterController>()) isTouching = false;
    }

    // Coroutine to exit chess puzzle mode
    IEnumerator Unplay()
    {
        isPlaying = false;
        playerCamera.transform.DOMove(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), 2);
        playerCamera.transform.DORotate(new Vector3(0.0f, 0.0f, 0.0f), 2);
        isSwitching = true;

        yield return new WaitForSeconds(2.0f);

        isSwitching = false;
        playerMovement.enabled = true;
        inventory.enabled = true;
        flashlight.SetActive(true);
        UI.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Coroutine to enter chess puzzle mode
    IEnumerator Play()
    {
        isPlaying = true;
        playerMovement.enabled = false;
        inventory.enabled = false;
        flashlight.SetActive(false);
        footsteps.Pause();
        UI.enabled = false;
        playerCamera.transform.DOMove(new Vector3(-12.31f, 1.75f, 12.7f), 2);
        playerCamera.transform.DORotate(new Vector3(90.0f, 0.0f, 0.0f), 2);
        isSwitching = true;

        yield return new WaitForSeconds(2.0f);

        isSwitching = false;
        Cursor.lockState = CursorLockMode.None;
    }

    // Coroutine to handle chess puzzle logic
    IEnumerator Puzzle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(componentCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                // Check if the clicked object is a chess piece
                if (hit.transform.name.Contains("Black") || hit.transform.name.Contains("White"))
                {
                    Physics.Raycast(hit.transform.position, -Vector3.up, out RaycastHit hit2);
                    square = hit2.transform.gameObject; // Get the square beneath the chess piece
                }
                else
                {
                    square = hit.transform.gameObject;
                }

                // Handle chess puzzle moves
                if (!firstMoveDone)
                {
                    if (lastClicked == "19" && square.name == "17")
                    {
                        // Move chess pieces with animations
                        pieceWhite1.transform.DOMoveX(-12.537f, 2.0f);

                        yield return new WaitForSeconds(2.0f);

                        pieceBlack1.transform.DOMoveX(-12.641f, 2.0f);

                        yield return new WaitForSeconds(2.0f);

                        firstMoveDone = true;
                    }
                }
                else if (!secondMoveDone)
                {
                    if (lastClicked == "24" && square.name == "42")
                    {
                        // Move chess pieces with animations
                        pieceWhite2.transform.DOMove(new Vector3(-12.45f, 1.3315f, 12.826f), 2.0f);

                        yield return new WaitForSeconds(1.6f);

                        pieceBlack2.GetComponent<Renderer>().enabled = false;

                        yield return new WaitForSeconds(0.4f);

                        pieceBlack3.transform.DOMove(new Vector3(-12.45f, 1.3315f, 12.826f), 2.0f);

                        yield return new WaitForSeconds(1.0f);

                        pieceWhite2.GetComponent<Renderer>().enabled = false;

                        yield return new WaitForSeconds(1.0f);

                        secondMoveDone = true;
                    }
                }
                else
                {
                    if (lastClicked == "3" && square.name == "59")
                    {
                        // Move chess pieces with animations
                        pieceWhite3.transform.DOMoveZ(13.022f, 2.0f);

                        yield return new WaitForSeconds(1.7f);

                        pieceBlack4.GetComponent<Renderer>().enabled = false;

                        yield return new WaitForSeconds(0.3f);

                        // Add chess puzzle completion event to the diary
                        diary.events.Add("chess");
                        StartCoroutine(Unplay());
                    }
                }
                lastClicked = square.name; // Update the last clicked square
            }
        }
    }


    bool HasInteracted()
    {
        // Detect if key is pressed 
        switch (PlayerPrefs.GetString("interact"))
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
                if (Input.GetKey(PlayerPrefs.GetString("interact")))
                {
                    return true;
                }
                break;
        }
        return false;
    }
}
