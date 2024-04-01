using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Diary : MonoBehaviour
{
    // Reference to the first text element
    [SerializeField] TMP_Text text1;

    // Reference to the second text element
    [SerializeField] TMP_Text text2;

    // Sound clip when a notification is added
    [SerializeField] AudioClip notificationSound;

    // Sound clip for writing effect
    [SerializeField] AudioClip writingSound;

    // Sound clip for turning pages
    [SerializeField] AudioClip pageSound;

    // Writing speed parameter
    [SerializeField, Range(0.005f, 0.05f)] float writingSpeed = 0.01f;

    // List to store game events
    readonly List<string> events = new();

    // List to store written events in the diary
    readonly List<string> writtenEvents = new();

    Canvas canvas;
    AudioSource sound;
    bool isBusy; // Flag to check if the diary is currently writing or turning pages
    int pageNumber; // Current page number

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        sound = GetComponent<AudioSource>();
        pageNumber = 0;

        events.Add("start");
    }

    // Update is called once per frame
    void Update()
    {
        sound.volume = PlayerPrefs.GetFloat("SFX");

        if (ToggleActions.IsPressed("diary"))
        {
            if (!canvas.enabled)
            {
                if (!UIState.isBusy) ShowDiary();
            }
            else
            {
                HideDiary();
            }
        }

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
            CheckEvents();
        }
    }

    // Coroutine to write text in the diary
    IEnumerator Write(string sentence)
    {
        isBusy = true;
        sound.clip = writingSound;
        sound.loop = true;
        sound.Play();

        if (text1.text == "")
        {
            foreach (char letter in sentence)
            {
                text1.text += letter;
                yield return new WaitForSeconds(writingSpeed);
            }
            sound.Stop();
            sound.loop = false;

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
            sound.loop = false;

            yield return new WaitForSeconds(2.0f);

            writtenEvents.Add(sentence);
            isBusy = false;
        }
        else
        {
            text1.text = "";
            text2.text = "";

            sound.clip = pageSound;
            sound.loop = false;
            sound.Play();

            yield return new WaitForSeconds(0.5f);

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

        pageNumber += n;
        isBusy = false;
    }

    // Toggle diary visibility and player movement
    void ShowDiary()
    {
        canvas.enabled = true;
        UIState.isBusy = true;
        ChangePlayerState.Disable();
    }

    void HideDiary()
    {
        canvas.enabled = false;
        UIState.isBusy = false;
        ChangePlayerState.Enable();
    }

    public int GetEventsCount()
    {
        return events.Count;
    }

    public bool IsEventAdded(string eventName)
    {
        return events.Contains(eventName) || writtenEvents.Contains(eventName);
    }

    public void AddEvent(string eventName)
    {
        sound.clip = notificationSound;
        sound.Play();
        events.Add(eventName);
    }

    // Check for specific game events and display corresponding diary entries
    void CheckEvents()
    {
        if (events.Contains("start"))
        {
            StartCoroutine(Write("Where did Pixelle go? I last saw her running away to that kind of hospital. Hope she is fine."));
            events.Remove("start");
        }
        else if (events.Contains("archEnter"))
        {
            StartCoroutine(Write("OH NO! I can't go out... Huh, let's find Pixelle before thinking about that."));
            events.Remove("archEnter");
        }
        else if (events.Contains("doorLock"))
        {
            StartCoroutine(Write("I don't have the key, i need to find it"));
            events.Remove("doorLock");
        }
        else if (events.Contains("rustyKey"))
        {
            StartCoroutine(Write("Berk, why was that key in that body? So disgusting! And how did it get so rusty?"));
            events.Remove("rustyKey");
        }
        else if (events.Contains("indoor"))
        {
            StartCoroutine(Write("Finally inside... Glad I don't have to touch that key anymore. Where is Pixelle though?"));
            events.Remove("indoor");
        }
        else if (events.Contains("lightCorridor"))
        {
            StartCoroutine(Write("What is illuminating the ceiling ? So scary! "));
            events.Remove("lightCorridor");
        }
        else if (events.Contains("lightning"))
        {
            StartCoroutine(Write("What an astounding lightning! It scared me so badly!"));
            events.Remove("lightning");
        }
        else if (events.Contains("chess"))
        {
            StartCoroutine(Write("Why did I have to play chess in this place with that scary old mother? And what is it that she whispered? " + KeyEvents.chessCode + ", I wonder what it could be"));
            events.Remove("chess");
        }
        else if (events.Contains("firstFloor"))
        {
            StartCoroutine(Write("Oh, what a scary corridor, I hope no one is here..."));
            events.Remove("firstFloor");
        }
        else if (events.Contains("TV"))
        {
            StartCoroutine(Write("AH THAT SOUND, so noisy. I'll likely have an earache."));
            events.Remove("TV");
        }
        else if (events.Contains("sewers"))
        {
            StartCoroutine(Write("Wah, it's so dark in here. Thankfully I've got that flashlight"));
            events.Remove("sewers");
        }
    }
}
