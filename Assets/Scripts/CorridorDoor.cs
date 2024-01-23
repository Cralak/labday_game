using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorDoor : MonoBehaviour
{
    bool isTouching; // Flag to check if the player is touching the door
    bool close; // Flag to determine if the door is currently closed

    // Start is called before the first frame update
    void Start()
    {
        isTouching = false;
        close = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is touching the door and presses the 'E' key
        if (isTouching && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the door state between open and closed
            if (close)
            {
                // Move the door to open position
                transform.position += new Vector3(5.0f, 0.0f, 3.5f);
                close = !close;
            }
            else
            {
                // Move the door to closed position
                transform.position += new Vector3(-5.0f, 0.0f, -3.5f);
                close = !close;
            }
        }
    }

    // Triggered when another collider enters the trigger zone
    void OnTriggerEnter(Collider collision)    
    {
        isTouching = true;
    }

    // Triggered when another collider exits the trigger zone
    void OnTriggerExit(Collider collision)   
    {
        isTouching = false;
    }
}
