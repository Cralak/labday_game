using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerCamera;
    [SerializeField] Light lightComponent;

    Inventory inventoryScript;

    // Start is called before the first frame update
    void Start()
    {
        inventoryScript = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            lightComponent.enabled = !lightComponent.enabled;
        }
    }
}
