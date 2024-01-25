using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Diary : MonoBehaviour
{
    // List to store game events
    public List<string> events = new();

    // Reference to the first text element
    [SerializeField] TMP_Text text1;

    // Reference to the second text element
    [SerializeField] TMP_Text text2;

    // Sound clip for writing effect
    [SerializeField] AudioClip writingSound;

    // Sound clip for turning pages
    [SerializeField] AudioClip pageSound;

    // Writing speed parameter
    [SerializeField, Range(0.005f, 0.05f)] float writingSpeed = 0.01f;

    // List to store written events in the diary
    readonly List<string> writtenEvents = new();

    GameObject player;
    PlayerMovement playerMovement;
    AudioSource footsteps;
    Inventory inventory;
    Canvas canvas;
    AudioSource sound;
    bool isBusy; // Flag to check if the diary is currently writing or turning pages
    int pageNumber; // Current page number

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        footsteps = player.GetComponent<AudioSource>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        canvas = GetComponent<Canvas>();
        sound = GetComponent<AudioSource>();
        pageNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        sound.volume = PlayerPrefs.GetFloat("SFX");

        ChangeDiaryState();

        // Check if the diary is open and not busy
        if (canvas.enabled && !isBusy)
        {
            // Turn pages with left and right arrow keys
            if (pageNumber > 0 && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                isBusy = true;
                StartCoroutine(TurnPage(-1));
            }

            if (pageNumber * 2 + 2 < writtenEvents.Count && Input.GetKeyDown(KeyCode.RightArrow))
            {
                isBusy = true;
                StartCoroutine(TurnPage(1));
            }

            int leftPage = pageNumber * 2;
            int rightPage = pageNumber * 2 + 1;

            // Display text on left and right pages
            text1.text = writtenEvents.Count > leftPage ? writtenEvents[leftPage] : "";
            text2.text = writtenEvents.Count > rightPage ? writtenEvents[rightPage] : "";

            // Check for specific game events and display corresponding diary entries
            if (events.Contains("start"))
            {
                isBusy = true;
                StartCoroutine(Write("Where did Pixelle go? I last saw her running away to that kind of hospital. Hope she is fine."));
                events.Remove("start");
            }
            else if (events.Contains("archEnter"))
            {
                isBusy = true;
                StartCoroutine(Write("OH NO! I can't go out... Why did it take so long to enter by the way?"));
                events.Remove("archEnter");
            }
            else if (events.Contains("doorLock"))
            {
                isBusy = true;
                StartCoroutine(Write("I don't have the key, i need to find it"));
                events.Remove("doorLock");
            }
            else if (events.Contains("rustyKey"))
            {
                isBusy = true;
                StartCoroutine(Write("Berk, why was that key in that body? So disgusting! And how did it get so rusty?"));
                events.Remove("rustyKey");
            }
            else if (events.Contains("indoor"))
            {
                isBusy = true;
                StartCoroutine(Write("Finally inside... Glad I don't have to touch that key anymore. Where is Pixelle though?"));
                events.Remove("indoor");
            }
            else if (events.Contains("lightCorridor"))
            {
                isBusy = true;
                StartCoroutine(Write("What is illuminating the ceiling ? So scary! "));
                events.Remove("lightCorridor");
            }
            else if (events.Contains("lightning"))
            {
                isBusy = true;
                StartCoroutine(Write("What an astounding lightning! It scared me so badly!"));
                events.Remove("lightning");
            }
            else if (events.Contains("chess"))
            {
                isBusy = true;
                StartCoroutine(Write("Why did I have to play chess in this place?"));
                events.Remove("chess");
            }
        }
    }

    // Coroutine to write text in the diary
    IEnumerator Write(string sentence)
    {
        sound.clip = writingSound;
        sound.Play();

        if (text1.text == "")
        {
            foreach (char letter in sentence)
            {
                text1.text += letter;
                yield return new WaitForSeconds(writingSpeed);
            }
            sound.Stop();

            yield return new WaitForSeconds(2.0f);

            writtenEvents.Add(sentence);
            isBusy = false;
        }
        else if (text2.text == "")
        {
            foreach (char letter in sentence)
            {
                text2.text += letter;
                yield return new WaitForSeconds(writingSpeed);
            }
            sound.Stop();

            yield return new WaitForSeconds(2.0f);

            writtenEvents.Add(sentence);
            isBusy = false;
        }
        else
        {
            text1.text = "";
            text2.text = "";

            sound.clip = pageSound;
            sound.Play();

            yield return new WaitForSeconds(0.5f);

            sound.Stop();
            pageNumber += 1;

            StartCoroutine(Write(sentence));
        }
    }

    // Coroutine to turn diary pages
    IEnumerator TurnPage(int n)
    {
        sound.clip = pageSound;
        sound.Play();

        yield return new WaitForSeconds(0.5f);

        sound.Stop();
        pageNumber += n;
        isBusy = false;
    }

    // Toggle diary visibility and player movement
    void ToggleDiary()
    {
        canvas.enabled = !canvas.enabled;
        if (canvas.enabled)
        {
            playerMovement.enabled = false;
            inventory.enabled = false;
            footsteps.Pause();
        }
        else
        {
            playerMovement.enabled = true;
            inventory.enabled = true;
            footsteps.UnPause();
        }
    }

    void ChangeDiaryState()
    {
        // Toggle the diary on/off with the diary key
        switch (PlayerPrefs.GetString("diary"))
        {
            case "escape":
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ToggleDiary();
                }
                break;
            case "tab":
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    ToggleDiary();
                }
                break;
            case "lock":
                if (Input.GetKeyDown(KeyCode.CapsLock))
                {
                    ToggleDiary();
                }
                break;
            case "backspace":
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    ToggleDiary();
                }
                break;
            case "return":
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    ToggleDiary();
                }
                break;
            case "space":
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ToggleDiary();
                }
                break;
            case "shift":
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    ToggleDiary();
                }
                break;
            case "alt":
                if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
                {
                    ToggleDiary();
                }
                break;
            case "control":
                if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                {
                    ToggleDiary();
                }
                break;
            case "meta":
                if (Input.GetKeyDown(KeyCode.LeftMeta) || Input.GetKeyDown(KeyCode.RightMeta))
                {
                    ToggleDiary();
                }
                break;
            case "upArrow":
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ToggleDiary();
                }
                break;
            case "downArrow":
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ToggleDiary();
                }
                break;
            case "leftArrow":
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    ToggleDiary();
                }
                break;
            case "rightArrow":
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    ToggleDiary();
                }
                break;
            case "leftClick":
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    ToggleDiary();
                }
                break;
            case "rightClick":
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    ToggleDiary();
                }
                break;
            case "wheelClick":
                if (Input.GetKeyDown(KeyCode.Mouse2))
                {
                    ToggleDiary();
                }
                break;
            default:
                if (Input.GetKeyDown(PlayerPrefs.GetString("diary")))
                {
                    ToggleDiary();
                }
                break;
        }
    }
}
