using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HospitalEnter : MonoBehaviour
{
    [SerializeField] GameObject key; // Reference to the key GameObject

    GameObject player;
    PlayerMovement playerMovement;
    Diary diary;
    bool isTouching; // Flag to check if the player is touching the trigger area
    Canvas text;
    Inventory inventoryScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        diary = GameObject.Find("Diary").GetComponent<Diary>();
        inventoryScript = player.GetComponent<Inventory>();
        isTouching = false;
        text = GetComponent<Canvas>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is touching the trigger area, has the key, and presses the "e" key
        if (isTouching && inventoryScript.inventory.Contains(key) && Input.GetKeyDown("e"))
        {
            StartCoroutine(LoadHospital());
        }

        // Check if the player is touching the trigger area, doesn't have the key, and presses the "e" key
        if (isTouching && !inventoryScript.inventory.Contains(key) && Input.GetKeyDown("e"))
        {
            diary.events.Add("doorLock");
        }
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
}
