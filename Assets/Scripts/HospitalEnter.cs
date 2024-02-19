using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HospitalEnter : MonoBehaviour
{
    [SerializeField] GameObject key; // Reference to the key GameObject

    GameObject player;
    PlayerMovement playerMovement;
    Diary diary;
    Inventory inventoryScript;
    bool isTouching; // Flag to check if the player is touching the trigger area
    bool firstTry; // To check if player already tried to enter
    Canvas text;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        inventoryScript = GameObject.Find("Inventory").GetComponent<Inventory>();
        text = GetComponent<Canvas>();
        text.enabled = false;
        isTouching = false;
        firstTry = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is touching the trigger area, and presses the "e" key
        if (isTouching && ToggleActions.IsPressed("interact")) Enter();
    }

    // Called when another collider enters the trigger area
    void OnTriggerEnter()
    {
        isTouching = true;
        text.enabled = true;
    }

    // Called when another collider exits the trigger area
    void OnTriggerExit()
    {
        isTouching = false;
        text.enabled = false;
    }

    // Coroutine to load the hospital scene
    IEnumerator LoadHospital()
    {
        playerMovement.enabled = false;
        player.transform.position = new Vector3(4.0f, 1.0f, 2.0f);
        inventoryScript.inventory.Remove(key);

        yield return new WaitForSeconds(0.1f);

        diary.events.Add("indoor");
        playerMovement.enabled = true;
        SceneManager.LoadScene("IndoorScene");
    }

    void Enter()
    {
        // Check if the player has the key in inventory
        if (inventoryScript.inventory.Contains(key))
        {
            StartCoroutine(LoadHospital());
        }
        else if (firstTry)
        {
            diary.events.Add("doorLock");
            firstTry = false;
        }
    }
}
