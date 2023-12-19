using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    [SerializeField]GameObject player;
    bool isColliding = false;
    Inventory2 inventoryScript;

    void Start()
    {
        inventoryScript = player.GetComponent<Inventory2>();
    }
    
    void Update()
    {
        if (isColliding && Input.GetKeyDown("e"))
        {
            inventoryScript.inventory.Add(gameObject);
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            isColliding = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == player)
        {
            isColliding = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == player)
        {
            isColliding = false;
        }
    }
}
