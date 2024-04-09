using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light lightComponent; // Reference to the Light component

    // Update is called once per frame
    void Update()
    {
        if (ToggleActions.IsPressed("flashlight")) lightComponent.enabled = !lightComponent.enabled;
    }
}

