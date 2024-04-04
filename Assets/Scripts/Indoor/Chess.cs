using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Chess : MonoBehaviour
{
    public bool isPlaying; // Flag to check if the chess puzzle is currently in progress

    // References to chess pieces GameObjects
    [SerializeField] GameObject board;
    [SerializeField] GameObject pieceWhite1;
    [SerializeField] GameObject pieceWhite2;
    [SerializeField] GameObject pieceWhite3;
    [SerializeField] GameObject pieceBlack1;
    [SerializeField] GameObject pieceBlack2;
    [SerializeField] GameObject pieceBlack3;
    [SerializeField] GameObject pieceBlack4;

    // References to other game objects and components
    GameObject player;
    GameObject playerCamera;
    Camera componentCamera;
    GameObject flashlight;
    Diary diary;
    Canvas UI;
    TMP_Text text;
    Light boardLight;

    bool isTouching;
    bool isSwitching;
    Vector3 initialRotation;
    GameObject square;
    string lastClicked;
    GameObject destinationSquare;
    bool firstMoveDone;
    bool secondMoveDone;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize references to game objects and components
        flashlight = GameObject.Find("Flashlight");
        playerCamera = flashlight.transform.parent.gameObject;
        player = playerCamera.transform.parent.gameObject;
        componentCamera = playerCamera.GetComponent<Camera>();
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        UI = GameObject.Find("UI").GetComponent<Canvas>();
        text = GetComponentInChildren<TMP_Text>();
        boardLight = board.transform.parent.GetComponentInChildren<Light>();
        isTouching = false;
        isPlaying = false;
        isSwitching = false;
        lastClicked = "64"; // Initial value to avoid null reference
    }

    // Update is called once per frame
    void Update()
    {
        text.enabled = isTouching;

        // Check if chess puzzle is not in progress
        if (!isPlaying)
        {
            // Check if player is touching and not currently switching, and interact key is pressed
            if (isTouching && !isSwitching && !UIState.isBusy && ToggleActions.IsPressed("interact")) StartCoroutine(Play()); // Start chess puzzle
        }
        else
        {
            StartCoroutine(Puzzle()); // Continue puzzle logic

            // Check if player is touching and not currently switching, and interact key is pressed
            if (isTouching && !isSwitching && ToggleActions.IsPressed("interact")) StartCoroutine(Unplay()); // End chess puzzle
        }
    }

    // Triggered when another collider enters the trigger zone
    void OnTriggerEnter(Collider collider)
    {
        isTouching = true;
    }

    // Triggered when another collider exits the trigger zone
    void OnTriggerExit(Collider collider)
    {
        isTouching = false;
    }

    // Coroutine to exit chess puzzle mode
    IEnumerator Unplay()
    {
        boardLight.enabled = false;
        isPlaying = false;
        playerCamera.transform.DOMove(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), 2.0f);
        playerCamera.transform.DORotate(initialRotation, 2.0f);
        isSwitching = true;

        yield return new WaitForSeconds(2.0f);

        isSwitching = false;
        UIState.isBusy = false;
        UI.enabled = true;
        ChangePlayerState.Enable();
        flashlight.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Coroutine to enter chess puzzle mode
    IEnumerator Play()
    {
        isSwitching = true;
        initialRotation = playerCamera.transform.eulerAngles;
        UI.enabled = false;
        UIState.isBusy = true;
        ChangePlayerState.Disable();
        flashlight.SetActive(false);
        playerCamera.transform.DOMove(board.transform.position + new Vector3(0.0f, 0.6f, 0.0f), 2.0f);
        playerCamera.transform.DORotate(new Vector3(90.0f, board.transform.eulerAngles.y, 0.0f), 2.0f);

        yield return new WaitForSeconds(2.0f);

        boardLight.enabled = true;
        isPlaying = true;
        Cursor.lockState = CursorLockMode.None;
        isSwitching = false;
    }

    // Coroutine to handle chess puzzle logic
    IEnumerator Puzzle()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
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
                        destinationSquare = GameObject.Find("17");
                        pieceWhite1.transform.DOMove(new Vector3(destinationSquare.transform.position.x, pieceWhite1.transform.position.y, destinationSquare.transform.position.z), 2.0f);

                        yield return new WaitForSeconds(2.0f);
                        
                        destinationSquare = GameObject.Find("56");
                        pieceBlack1.transform.DOMove(new Vector3(destinationSquare.transform.position.x, pieceBlack1.transform.position.y, destinationSquare.transform.position.z), 2.0f);

                        yield return new WaitForSeconds(2.0f);

                        firstMoveDone = true;
                    }
                }
                else if (!secondMoveDone)
                {
                    if (lastClicked == "24" && square.name == "42")
                    {
                        // Move chess pieces with animations
                        destinationSquare = GameObject.Find("42");
                        pieceWhite2.transform.DOMove(new Vector3(destinationSquare.transform.position.x, pieceWhite2.transform.position.y, destinationSquare.transform.position.z), 2.0f);

                        yield return new WaitForSeconds(1.6f);

                        pieceBlack2.GetComponent<Renderer>().enabled = false;

                        yield return new WaitForSeconds(0.4f);

                        // Move chess pieces with animations
                        destinationSquare = GameObject.Find("42");
                        pieceBlack3.transform.DOMove(new Vector3(destinationSquare.transform.position.x, pieceBlack3.transform.position.y, destinationSquare.transform.position.z), 2.0f);

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
                        destinationSquare = GameObject.Find("59");
                        pieceWhite3.transform.DOMove(new Vector3(destinationSquare.transform.position.x, pieceWhite3.transform.position.y, destinationSquare.transform.position.z), 2.0f);

                        yield return new WaitForSeconds(1.7f);

                        pieceBlack4.GetComponent<Renderer>().enabled = false;

                        yield return new WaitForSeconds(0.3f);

                        // Add chess puzzle completion event to the diary
                        diary.AddEvent("chess");
                        StartCoroutine(Unplay());
                    }
                }
                lastClicked = square.name; // Update the last clicked square
            }
        }
    }
}
