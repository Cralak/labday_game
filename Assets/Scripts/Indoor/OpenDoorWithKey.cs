using DG.Tweening;
using UnityEngine;

public class OpenDoorWithKey : MonoBehaviour
{
    GameObject player;
    Diary diary;
    GameObject key;
    AudioSource doorNoise;
    bool isColliding;
    Inventory inventoryScript;

    // Start is called before the first frame update    
    void Start()
    {
        // Find the Player, Diary, Key, and Inventory components
        player = GameObject.Find("Player");
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        key = GameObject.Find("Key");
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
        if (isColliding && !UIState.isBusy && inventoryScript.inventory.Contains(key) && ToggleActions.IsPressed("interact"))
        {
            // Remove the key from the inventory
            inventoryScript.inventory.Remove(key);

            // Move the door using DOTween animation
            transform.DOMove(transform.position + new Vector3(5.0f, 0.0f, 3.5f), 5.0f);

            // Play the door opening sound
            doorNoise.Play();

            // Add an event to the diary
            diary.AddEvent("rustyKey");
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
}
