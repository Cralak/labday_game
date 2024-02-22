using UnityEngine;
using UnityEngine.UI;

public class ArchEntrance : MonoBehaviour
{
    GameObject player; // Reference to the player GameObject
    Diary diary; // Reference to the Diary script
    bool isTouching; // Flag to check if the player is touching the trigger area
    Canvas text; // Reference to another Canvas component (possibly for displaying text)

    // Start is called before the first frame update
    void Start()
    {
        // Find and assign references to necessary components and objects
        player = GameObject.Find("Player");
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        text = GetComponent<Canvas>();
        isTouching = false;
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is touching and presses the interact key
        if (isTouching && ToggleActions.IsPressed("interact")) Enter();
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

    void Enter()
    {
        StartCoroutine(ChangeScene.GoTo(player, new Vector3(22, 1, 50)));

        // Add corresponding event in the diary
        diary.AddEvents("archEnter");

        // Reset the touching flag
        isTouching = false;
    }
}