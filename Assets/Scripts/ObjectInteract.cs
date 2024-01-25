using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    GameObject player;
    Diary diary;
    bool isColliding = false;
    Inventory inventoryScript;

    void Start()
    {
        // Find the Player, Diary, and Inventory components
        player = GameObject.Find("Player");
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        inventoryScript = player.GetComponent<Inventory>();
    }

    void Update()
    {
        // Check if colliding
        if (isColliding)
        {
            Interact();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        // Set the colliding flag when the player enters the trigger zone
        if (collider.gameObject == player) isColliding = true;
    }

    void OnTriggerExit(Collider collider)
    {
        // Reset the colliding flag when the player exits the trigger zone
        if (collider.gameObject == player) isColliding = false;
    }

    void TakeObject()
    {
        // Add the game object to the inventory
        inventoryScript.inventory.Add(gameObject);

        // Set the scale to zero to hide the object
        transform.localScale = new Vector3(0, 0, 0);

        // Disable the collider
        GetComponent<Collider>().enabled = false;

        // Reset the colliding flag and disable the script
        isColliding = false;
        enabled = false;

        // If the object is a key, add an event to the diary
        if (name == "Key")
        {
            diary.events.Add("rustyKey");
        }
    }

    void Interact()
    {
        // Take the object with interact key
        switch (PlayerPrefs.GetString("interact"))
        {
            case "escape":
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    TakeObject();
                }
                break;
            case "tab":
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    TakeObject();
                }
                break;
            case "lock":
                if (Input.GetKeyDown(KeyCode.CapsLock))
                {
                    TakeObject();
                }
                break;
            case "backspace":
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    TakeObject();
                }
                break;
            case "return":
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    TakeObject();
                }
                break;
            case "space":
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    TakeObject();
                }
                break;
            case "shift":
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    TakeObject();
                }
                break;
            case "alt":
                if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
                {
                    TakeObject();
                }
                break;
            case "control":
                if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                {
                    TakeObject();
                }
                break;
            case "meta":
                if (Input.GetKeyDown(KeyCode.LeftMeta) || Input.GetKeyDown(KeyCode.RightMeta))
                {
                    TakeObject();
                }
                break;
            case "upArrow":
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    TakeObject();
                }
                break;
            case "downArrow":
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    TakeObject();
                }
                break;
            case "leftArrow":
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    TakeObject();
                }
                break;
            case "rightArrow":
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    TakeObject();
                }
                break;
            case "leftClick":
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    TakeObject();
                }
                break;
            case "rightClick":
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    TakeObject();
                }
                break;
            case "wheelClick":
                if (Input.GetKeyDown(KeyCode.Mouse2))
                {
                    TakeObject();
                }
                break;
            default:
                if (Input.GetKeyDown(PlayerPrefs.GetString("interact")))
                {
                    TakeObject();
                }
                break;
        }
    }
}
