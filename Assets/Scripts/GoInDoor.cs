using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoIndoor : MonoBehaviour
{
    bool isTouching; // Flag to check if the player is touching the trigger area

    // Start is called before the first frame update
    void Start()
    {
        isTouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is touching the trigger area and presses the "e" key
        if (isTouching && Input.GetKeyDown("e"))
        {
            print("e press");
            // Load the "indoorScene"
            SceneManager.LoadScene("indoorScene");
        }
    }

    // Called when another collider enters the trigger area
    void OnTriggerEnter(Collider collision)    
    {
        print("touch");
        isTouching = true;
    }

    // Called when another collider exits the trigger area
    void OnTriggerExit(Collider collision)   
    {
        isTouching = false;
    }
}
