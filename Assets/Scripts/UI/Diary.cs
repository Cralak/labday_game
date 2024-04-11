using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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

    // List to match events and their sentences
    readonly Dictionary<string, string> eventsTexts = new() {
        {"start", "Where did Pixelle go? What a messy dog... I last saw her running away to that kind of hospital. Hope she is fine."},
        {"doorLock", "I don't have the key, i need to find it."},
        {"RustyKey", "Berk, why was that key in that body? So disgusting!"},
        {"indoor", "Finally inside... Glad I don't have to touch that key anymore. Where is Pixelle though?"},
        {"Axe", "Nice, an Axe! I feel like I could destroy everything now"},
        {"findCrowbar", "Can't take off the planks, I need to find a tool to take them off."},
        {"firstFloorDoor", "Looks like the code worked! Let's check what's in there!"},
        {"firstFloor", "Oh, what a scary corridor, I hope no one is here..."},
        {"firstFloorExit", "I have the feeling that I saw something... I'm not reassured with everything that's happening here. I should go back to keep an eye on the lady from earlier, she worries me."},
        {"ladyDisappeared", "Wait... Where has she gone? I'm shitting myself."},
        {"InfirmaryKey", "It's definitely the key to the infirmary that I saw up there, but I don't want to go back. Just thinking about it scares me, but I don't really have a choice."},
        {"infirmaryDoorDisappeared", "The door is opened? How? Am I becoming mentally ill?"},
        {"basementDoor", "I'm starting to hear weird sounds, it's like someone is near. Let's hurry."}};

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
