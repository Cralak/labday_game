using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HospitalEnter : MonoBehaviour
{
    [SerializeField] GameObject key; // Reference to the key GameObject
    [SerializeField] Canvas canvas; // Reference to the Canvas component
    [SerializeField] AudioClip BGMClip; // Reference to the Canvas component

    Image blackScreen; // Reference to the Image component for the black screen effect
    GameObject player;
    PlayerMovement playerMovement;
    AudioSource footsteps;
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
        footsteps = player.GetComponent<AudioSource>();
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        inventoryScript = GameObject.Find("Inventory").GetComponent<Inventory>();
        blackScreen = canvas.GetComponentInChildren<Image>();
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

    void CreateIndoorBGM()
    {
        GameObject music = new("BGM");
        AudioSource BGMSource = music.AddComponent<AudioSource>();
        BGMSource.clip = BGMClip;
        BGMSource.loop = true;
        BGMSource.Play();
        music.AddComponent<SetSFXVolume>();
        DontDestroyOnLoad(music);
    }

    // Coroutine to load the hospital scene
    IEnumerator LoadHospital()
    {
        playerMovement.enabled = false;
        Color c = blackScreen.color;
        c.a = 255;
        blackScreen.color = c;
        player.transform.position = new Vector3(0f, 1.0f, -12.0f);
        inventoryScript.inventory.Remove(key);
        footsteps.Pause();

        yield return new WaitForSeconds(0.1f);

        diary.AddEvents("indoor");
        playerMovement.enabled = true;
        CreateIndoorBGM();
        SceneManager.LoadScene("TestIndoor");
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
            diary.AddEvents("doorLock");
            firstTry = false;
        }
    }
}
