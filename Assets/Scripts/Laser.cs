using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Light laser; // Reference to the Light component

    void Start()
    {
        laser = GetComponent<Light>();
    }

    void Update()
    {
        // Toggle the laser on/off with the L key
        if (Input.GetKeyDown(KeyCode.L)) 
            laser.enabled = !laser.enabled;

        // Change the laser color periodically when it is enabled
        if (laser.enabled && Time.frameCount % 5 == 0) 
            laser.color = Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 0.0f, 1.0f);
    }
}
