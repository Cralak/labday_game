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
        if (isColliding)
        {
            if (ToggleActions.IsPressed("interact"))
            {
                if (!canvas.enabled) ShowPaper();
                else HidePaper();
            }
        }
    }

    void ShowPaper()
    {
        textField.text = text;
        canvas.enabled = true;
        ChangeActionsState.DisableAll();
    }

    void HidePaper()
    {
        canvas.enabled = false;
        ChangeActionsState.EnableAll();
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
