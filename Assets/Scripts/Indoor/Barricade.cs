using UnityEngine;

public class Barricade : MonoBehaviour
{
    [SerializeField] GameObject crowbar; // Reference to the key GameObject

    Diary diary;
    Inventory inventoryScript;
    AudioSource sound;
    bool isTouching = false; // Flag to check if the player is touching the trigger area
    bool isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();

        if (KeyEvents.barricade) Destroy(gameObject);

        inventoryScript = GameObject.Find("Inventory").GetComponent<Inventory>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is touching the trigger area, and presses the "e" key
        if (ToggleActions.IsPressed("interact") && isTouching && !UIState.isBusy)
        {
            if (inventoryScript.CheckInventory(crowbar))
            {
                KeyEvents.barricade = true;
                GetComponent<AudioSource>().Play();
                isDestroyed = true;
                transform.localScale = Vector3.zero;
            }
            else
            {
                if (!diary.CheckEvent("findCrowbar")) diary.AddEvent("findCrowbar");
            }
        }

        if (isDestroyed && !sound.isPlaying) Destroy(gameObject);
    }

    // Called when another collider enters the trigger area
    void OnTriggerEnter()
    {
        isTouching = true;
    }

    // Called when another collider exits the trigger area
    void OnTriggerExit()
    {
        isTouching = false;
    }
}
