using System.Collections;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    [SerializeField] Transform player;
    Canvas text;

    void Start()
    {
        InitializeComponents();
    }

    void Update()
    {
        UpdateTextOrientation();
    }

    void InitializeComponents()
    {
        // Initialize required components
        text = GetComponent<Canvas>();
        text.enabled = false;
    }

    void UpdateTextOrientation()
    {
        // Ensure the text canvas faces the player
        transform.LookAt(player);
    }

    void OnTriggerEnter(Collider collision)
    {
        // Toggle the visibility of the text canvas when triggered
        text.enabled = !text.enabled;
    }

    void OnTriggerExit(Collider collision)
    {
        // Toggle the visibility of the text canvas when exiting the trigger
        text.enabled = !text.enabled;
    }
}
