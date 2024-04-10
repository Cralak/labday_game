using UnityEngine;
using TMPro;

public class OpenInput : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Canvas text;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject nurseryDoorLeft;
    [SerializeField] GameObject nurseryWindowLeft;
    [SerializeField] GameObject nurseryDoorRight;
    [SerializeField] GameObject nurseryWindowRight;

    string answer;
    bool isTouching = false;
    bool isTyping = false;
    bool isResolved;
    Diary diary;


    // Start is called before the first frame update
    void Start()
    {
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        isTouching = false;
        isResolved = diary.CheckEvent("officeDoor");

        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching && ToggleActions.IsPressed("interact") && !isTyping && !isResolved)
        {
            isTyping = true;
            ChangePlayerState.Disable();
            canvas.enabled = !canvas.enabled;
            Cursor.lockState = CursorLockMode.None;
            UIState.isBusy = true;
        }
    }

    void OnTriggerEnter()
    {
        isTouching = true;
    }
    void OnTriggerExit()
    {
        isTouching = false;
    }

    public void Check()
    {
        answer = inputField.text;
        if (answer == KeyEvents.wordleCode)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 130, 0));
            Destroy(nurseryDoorLeft);
            Destroy(nurseryWindowLeft);
            Destroy(nurseryDoorRight);
            Destroy(nurseryWindowRight);
            isResolved = true;
            text.enabled = false;
            Exit();
        }
    }

    public void Exit()
    {
        isTyping = false;
        ChangePlayerState.Enable();
        canvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        UIState.isBusy = false;
    }
}
