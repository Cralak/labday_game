using DG.Tweening;
using UnityEngine;

public class OpenDoorWithKey : MonoBehaviour
{
    Diary diary;
    GameObject key;
    AudioSource doorNoise;
    bool isColliding;
    Inventory inventoryScript;

    // Start is called before the first frame update    
    void Start()
    {
        // Find the Diary, Key, and Inventory components
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        key = GameObject.Find("RustyKey");
        inventoryScript = GameObject.Find("Inventory").GetComponent<Inventory>();
        doorNoise = GetComponent<AudioSource>();

        isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Set the volume of the door noise based on player preferences
        doorNoise.volume = PlayerPrefs.GetFloat("SFX");

        // Check for collision, 'E' key press, and presence of the key in the inventory
<<<<<<< HEAD
        if (isColliding && !UIState.isBusy && inventoryScript.CheckInventory(key) && ToggleActions.IsPressed("interact"))
=======
        if (isColliding && !UIState.isBusy && inventoryScript.inventory.Contains(key) && ToggleActions.IsPressed("interact"))
>>>>>>> Cralak
        {
            // Remove the key from the inventory
            inventoryScript.RemoveInventory(key);

            // Move the door using DOTween animation
            transform.DOMove(transform.position + new Vector3(5.0f, 0.0f, 3.5f), 5.0f);

            // Play the door opening sound
            doorNoise.Play();

            // Add an event to the diary
<<<<<<< HEAD
            diary.AddEvent("RustyKey");
=======
            diary.AddEvent("rustyKey");
>>>>>>> Cralak
        }
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
}
