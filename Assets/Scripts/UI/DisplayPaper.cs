using TMPro;
using UnityEngine;

public class DisplayPaper : MonoBehaviour
{
    [SerializeField] string text;

    GameObject flyingPage;
    Canvas canvas;
    Canvas UI; // Reference to the main UI canvas
    Diary diary;
    TMP_Text textField;
    bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("UI").GetComponent<Canvas>();
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        flyingPage = GameObject.Find("FlyingPage");
        canvas = flyingPage.GetComponent<Canvas>();
        textField = flyingPage.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding && ToggleActions.IsPressed("interact"))
        {
            if (!canvas.enabled && !UIState.isBusy) ShowPaper();
            else HidePaper();
        }
    }

    void ShowPaper()
    {
        UIState.isBusy = true;
        ChangePlayerState.Disable();
        textField.text = text;
        UI.enabled = false;
        canvas.enabled = true;

        if (!diary.CheckEvent("basementCode") && name == "BasementCodePaper") diary.AddEvent("basementCode"); 
    }

    void HidePaper()
    {
        UIState.isBusy = false;
        ChangePlayerState.Enable();
        UI.enabled = true;
        canvas.enabled = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        // Set the colliding flag when the player enters the trigger zone
        isColliding = true;
    }

    void OnTriggerExit(Collider collider)
    {
        // Reset the colliding flag when the player exits the trigger zone
        isColliding = false;
    }

    public void SetText(string newText)
    {
        text = newText;
    }
}
