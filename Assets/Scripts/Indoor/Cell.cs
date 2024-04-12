using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] Vector3 endingPosition; // Position where player gets teleported to

    GameObject player; // Reference to the player GameObject
    bool isTouching; // Flag to check if the player is touching the trigger area

    // Start is called before the first frame update
    void Start()
    {
        // Find and assign references to necessary components and objects
        player = GameObject.Find("Player");
        isTouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is touching and presses the interact key
        if (isTouching && !UIState.isBusy && ToggleActions.IsPressed("interact"))
        {
            Enter();
        }
    }

    // Called when another Collider enters the trigger zone
    void OnTriggerEnter()
    {
        // Set the touching flag to true and enable the associated text Canvas
        isTouching = true;
    }

    // Called when another Collider exits the trigger zone
    void OnTriggerExit()
    {
        // Set the touching flag to false and disable the associated text Canvas
        isTouching = false;
    }

    void Enter()
    {
        StartCoroutine(Teleport.GoTo(player, endingPosition));

        // Reset the touching flag
        isTouching = false;
    }
}
