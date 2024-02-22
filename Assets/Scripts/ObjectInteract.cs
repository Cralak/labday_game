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
        inventoryScript = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    void Update()
    {
        // Check if colliding
        if (isColliding && ToggleActions.IsPressed("interact")) TakeObject();
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
            diary.AddEvent("rustyKey");
        }
    }
}
