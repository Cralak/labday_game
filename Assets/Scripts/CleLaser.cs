using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleLaser : MonoBehaviour
{
    Light laser; // Reference to the Light component attached to the GameObject

    void Start()
    {
        laser = GetComponent<Light>(); // Initialize the reference to the Light component
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the 'l' key is pressed
        if (Input.GetKeyDown("l"))
        {
            // Toggle the visibility of the laser by enabling/disabling the Light component
            laser.enabled = !laser.enabled;
        }

        // Check if the laser is currently enabled (visible)
        if (laser.enabled)
        {
            // Change the color of the laser every 5 frames
            if (Time.frameCount % 5 == 0)
            {
                // Set the laser color to a random color using the HSV color space
                laser.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f);
            }
        }
    }
}
