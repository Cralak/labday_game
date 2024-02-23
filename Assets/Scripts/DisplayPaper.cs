using TMPro;
using UnityEngine;

public class DisplayPaper : MonoBehaviour
{
    [SerializeField] string text;

    GameObject flyingPage;
    Canvas canvas;
    TMP_Text textField;
    bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        flyingPage = GameObject.Find("FlyingPage");
        canvas = flyingPage.GetComponent<Canvas>();
        textField = flyingPage.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding && !UIState.isBusy && ToggleActions.IsPressed("interact"))
        {
            if (!canvas.enabled) ShowPaper();
            else HidePaper();
        }
    }

    void ShowPaper()
    {
        UIState.isBusy = true;
        ChangePlayerState.Disable();
        textField.text = text;
        canvas.enabled = true;
    }

    void HidePaper()
    {
        UIState.isBusy = false;
        ChangePlayerState.Enable();
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
}
