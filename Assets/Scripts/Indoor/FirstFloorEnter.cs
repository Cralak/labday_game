using UnityEngine;

public class FirstFloorEnter : MonoBehaviour
{
    GameObject player;
    Diary diary;
    bool isTouching; // Flag to check if the player is touching the trigger area
    bool isInputing;
    Canvas areaText;
    Canvas digicodeCanvas;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        areaText = GetComponent<Canvas>();
        areaText.enabled = false;
        isTouching = false;
        digicodeCanvas = GameObject.Find("Digicode").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is touching the trigger area, and presses the "e" key
        if (diary.CheckEvent("firstFloorDoor"))
        {
            if (isTouching && !UIState.isBusy && ToggleActions.IsPressed("interact")) Enter();
        }
        else
        {
            if (!isInputing)
            {
                if (isTouching && !UIState.isBusy && ToggleActions.IsPressed("interact")) StartCodeInput();
            }
            else
            {
                if (ToggleActions.IsPressed("interact")) StopCodeInput();
            }
        }
    }

    // Called when another collider enters the trigger area
    void OnTriggerEnter()
    {
        isTouching = true;
        areaText.enabled = true;
    }

    // Called when another collider exits the trigger area
    void OnTriggerExit()
    {
        isTouching = false;
        areaText.enabled = false;
    }

    void StartCodeInput()
    {
        isInputing = true;
        UIState.isBusy = true;
        ChangePlayerState.Disable();
        Cursor.lockState = CursorLockMode.None;
        digicodeCanvas.enabled = true;
    }

    void StopCodeInput()
    {
        isInputing = false;
        UIState.isBusy = false;
        ChangePlayerState.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        digicodeCanvas.enabled = false;
    }

    void Enter()
    {
        if (!diary.CheckEvent("firstFloor")) diary.AddEvent("firstFloor");
        StartCoroutine(Teleport.GoTo(player, new Vector3(0f, 1f, 0f), "FirstFloor"));
    }
}
