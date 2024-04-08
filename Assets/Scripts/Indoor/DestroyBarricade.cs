using UnityEngine;

public class DestroyBarricade : MonoBehaviour
{
    [SerializeField] GameObject crowbar; // Reference to the key GameObject

    Diary diary;
    Inventory inventoryScript;
    bool isTouching; // Flag to check if the player is touching the trigger area

    // Start is called before the first frame update
    void Start()
    {
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        inventoryScript = GameObject.Find("Inventory").GetComponent<Inventory>();
        isTouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is touching the trigger area, and presses the "e" key
        if (inventoryScript.CheckInventory(crowbar) && ToggleActions.IsPressed("interact") && isTouching && !UIState.isBusy)
        {
            Destroy(gameObject);
            diary.AddEvent("barricade");
        }
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
