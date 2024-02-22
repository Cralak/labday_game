using UnityEngine;

public class HospitalEnter : MonoBehaviour
{
    [SerializeField] GameObject key; // Reference to the key GameObject
    [SerializeField] AudioClip BGMClip; // Reference to the Canvas component

    GameObject player;
    Diary diary;
    Inventory inventoryScript;
    bool isTouching; // Flag to check if the player is touching the trigger area
    bool firstTry; // To check if player already tried to enter
    Canvas text;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
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

    void Enter()
    {
        // Check if the player has the key in inventory
        if (inventoryScript.inventory.Contains(key))
        {
            inventoryScript.inventory.Remove(key);
            diary.AddEvents("indoor");
            CreateIndoorBGM();
            StartCoroutine(Teleport.GoTo(player, new Vector3(0.0f, 1.0f, -12.0f), "TestIndoor"));
        }
        else if (firstTry)
        {
            diary.AddEvents("doorLock");
            firstTry = false;
        }
    }
}
