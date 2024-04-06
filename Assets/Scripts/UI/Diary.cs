using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
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
    [SerializeField, Range(0.005f, 0.05f)] readonly float writingSpeed = 0.01f;

    // List to store game events
    readonly List<string> events = new();

    // List to store written events in the diary
    readonly List<string> writtenEvents = new();

    // List to match events and their sentences
    readonly Dictionary<string, string> eventsTexts = new() {
        {"start", "Where did Pixelle go? I last saw her running away to that kind of hospital. Hope she is fine."},
        {"lightning", "What an astounding lightning! It scared me so badly!"},
        {"archEnter", "OH NO! I can't go out... Huh, let's find Pixelle before thinking about that."},
        {"doorLock", "I don't have the key, i need to find it."},
        {"rustyKey", "Berk, why was that key in that body? So disgusting! And how did it get so rusty?"},
        {"indoor", "Finally inside... Glad I don't have to touch that key anymore. Where is Pixelle though?"},
        {"lightCorridor", "What is illuminating the ceiling ? So scary!"},
        {"firstFloorDoor", "I knew, the code was what the ghost said... How did she even know it?"},
        {"firstFloor", "Oh, what a scary corridor, I hope no one is here..."},
        {"TV", "AH THAT SOUND, so noisy. I'll likely have an earache."},
        {"basementDoor", "Finally reached the basement. What can be found here?"},
        {"sewers", "Wah, it's so dark in here. Thankfully I've got that flashlight."}};

    Canvas canvas;
    AudioSource sound;
    AudioSource notification;
    bool isBusy; // Flag to check if the diary is currently writing or turning pages
    int pageNumber; // Current page number

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        sound = GetComponent<AudioSource>();
        notification = gameObject.AddComponent<AudioSource>();
        notification.clip = notificationSound;
        pageNumber = 0;

        events.Add("start");
    }

    // Update is called once per frame
    void Update()
    {
        sound.volume = PlayerPrefs.GetFloat("SFX");
        notification.volume = PlayerPrefs.GetFloat("SFX");

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
            text1.text = writtenEvents.Count > leftPage ? eventsTexts[writtenEvents[leftPage]] : "";
            text2.text = writtenEvents.Count > rightPage ? eventsTexts[writtenEvents[rightPage]] : "";

            // Check for specific game events and display corresponding diary entries
            WriteEvents();
        }
    }

    // Coroutine to write text in the diary
    IEnumerator Write(string eventName)
    {
        sound.clip = writingSound;
        sound.loop = true;
        sound.Play();

        if (text1.text == "")
        {
            string sentence = eventsTexts[eventName];
            foreach (char letter in sentence)
            {
                text1.text += letter;
                yield return new WaitForSeconds(writingSpeed);
            }
            sound.Stop();
            sound.loop = false;

            yield return new WaitForSeconds(2.0f);

            events.Remove(eventName);
            writtenEvents.Add(eventName);
            isBusy = false;
        }
        else if (text2.text == "")
        {
            string sentence = eventsTexts[eventName];
            foreach (char letter in sentence)
            {
                text2.text += letter;
                yield return new WaitForSeconds(writingSpeed);
            }
            sound.Stop();
            sound.loop = false;

            yield return new WaitForSeconds(2.0f);

            events.Remove(eventName);
            writtenEvents.Add(eventName);
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

            StartCoroutine(Write(eventName));
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

    public bool CheckEvent(string eventName)
    {
        return events.Contains(eventName) || writtenEvents.Contains(eventName);
    }

    public void AddEvent(string eventName)
    {
        notification.Play();
        events.Add(eventName);
    }

    public void SetEventText(string eventName, string text)
    {
        eventsTexts[eventName] = text;
    }

    // Check for specific game events and display corresponding diary entries
    void WriteEvents()
    {
        foreach (string eventName in events)
        {
            if (!isBusy && eventsTexts.Keys.Contains(eventName))
            {
                StartCoroutine(Write(eventName));
                isBusy = true;
                break;
            }
        }
    }
}
