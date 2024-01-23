using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light lightComponent; // Reference to the Light component

    // Update is called once per frame
    void Update()
    {
        // Toggle the flashlight on/off with the F key
        if (Input.GetKeyDown(KeyCode.F)) 
            lightComponent.enabled = !lightComponent.enabled;
    }
}

