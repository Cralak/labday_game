using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    GameObject player;
    bool isColliding = false;
    Inventory inventoryScript;

    void Start()
    {
        player = GameObject.Find("Player");
        inventoryScript = player.GetComponent<Inventory>();
    }

    void Update()
    {
        if (isColliding && Input.GetKeyDown(KeyCode.E))
        {
            inventoryScript.inventory.Add(gameObject);
            transform.localScale = new Vector3(0, 0, 0);
            GetComponent<Collider>().enabled = false;
            isColliding = false;
            enabled = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == player) isColliding = true;
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == player) isColliding = false;
    }
}