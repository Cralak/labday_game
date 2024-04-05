using UnityEngine;

public class BasementEnter : MonoBehaviour
{
    [SerializeField] GameObject door;

    Diary diary;
    bool isTouching; // Flag to check if the player is touching the trigger area
    bool isInputing;
    Canvas areaText;
    Canvas digicodeCanvas;

    // Start is called before the first frame update
    void Start()
    {
        digicodeCanvas = GameObject.Find("Digicode").GetComponent<Canvas>();
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        areaText = GetComponent<Canvas>();
        areaText.enabled = false;
        isTouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is touching the trigger area, and presses the interact key
        if (!isInputing)
        {
            if (isTouching && !UIState.isBusy && ToggleActions.IsPressed("interact")) StartCodeInput();
        }
        else
        {
            if (ToggleActions.IsPressed("interact")) StopCodeInput();
        }

        if (diary.CheckEvent("basementDoor"))
        {
            door.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 130.0f, 0.0f));
            Destroy(gameObject);
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
}
