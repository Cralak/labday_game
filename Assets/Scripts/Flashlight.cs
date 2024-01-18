using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light lightComponent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) lightComponent.enabled = !lightComponent.enabled;
    }
}
