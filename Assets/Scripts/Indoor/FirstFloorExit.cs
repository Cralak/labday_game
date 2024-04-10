using UnityEngine;

public class FirstFloorExit : MonoBehaviour
{
    GameObject player;
    bool isTouching; // Flag to check if the player is touching the trigger area
    Canvas text;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        text = GetComponent<Canvas>();
        text.enabled = false;
        isTouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is touching the trigger area, and presses the "e" key
        if (isTouching && ToggleActions.IsPressed("interact")) Enter();
    }

    // Called when another collider enters the trigger area
    void OnTriggerEnter()
    {
        isTouching = true;
        text.enabled = true;
    }

    // Called when another collider exits the trigger area
    void OnTriggerExit()
    {
        isTouching = false;
        text.enabled = false;
    }

    void Enter()
    {
        player.transform.rotation = Quaternion.Euler(new Vector3(0,160,0));
        StartCoroutine(Teleport.GoTo(player, new Vector3(-24f, 4.1f, 1f), "Indoor"));
    }
}
