using UnityEngine;

public class TakeObject : MonoBehaviour
{
    [SerializeField] Texture2D texture2D;

    Diary diary;
    bool isColliding = false;
    Inventory inventoryScript;

    void Start()
    {
        // Find the Player, Diary, and Inventory components
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        inventoryScript = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    void Update()
    {
        // Check if colliding
<<<<<<<< HEAD:Assets/Scripts/Utils/TakeObject.cs
        if (isColliding && !UIState.isBusy && ToggleActions.IsPressed("interact")) PickUp();
========
        if (isColliding && !UIState.isBusy && ToggleActions.IsPressed("interact")) TakeObject();
>>>>>>>> Cralak:Assets/Scripts/Utils/ObjectInteract.cs
    }

    void OnTriggerEnter(Collider collider)
    {
        // Set the colliding flag when the player enters the trigger zone
        isColliding = true;
    }

    void OnTriggerExit(Collider collider)
    {
        // Reset the colliding flag when the player exits the trigger zone
        isColliding = false;
    }

    void PickUp()
    {
        // Add the game object to the inventory
        inventoryScript.AddInventory(gameObject);

        // Set how object is displayed in inventory
        inventoryScript.SetTexture2D(gameObject, texture2D);

        // Set the scale to zero to hide the object
        transform.localScale = new Vector3(0, 0, 0);

        // Disable the collider
        GetComponent<Collider>().enabled = false;

        // Reset the colliding flag and disable the script
        isColliding = false;
        enabled = false;

        // If the object is a key, add an event to the diary
<<<<<<<< HEAD:Assets/Scripts/Utils/TakeObject.cs
        diary.AddEvent(name);
        
========
        if (name == "Key")
        {
            diary.AddEvent("rustyKey");
        }
>>>>>>>> Cralak:Assets/Scripts/Utils/ObjectInteract.cs
    }
}
