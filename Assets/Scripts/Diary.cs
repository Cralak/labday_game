using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Diary : MonoBehaviour
{
    public List<string> events = new();

    [SerializeField] TMP_Text text1;
    [SerializeField] TMP_Text text2;
    [SerializeField] AudioClip writingSound;
    [SerializeField] AudioClip pageSound;
    [SerializeField, Range(0.005f, 0.05f)] float writingSpeed = 0.01f;

    readonly List<string> writtenEvents = new();

    GameObject player;
    PlayerMovement playerMovement;
    AudioSource footsteps;
    Canvas canvas;
    AudioSource sound;
    bool isBusy;
    int pageNumber;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        footsteps = player.GetComponent<AudioSource>();
        canvas = GetComponent<Canvas>();
        sound = GetComponent<AudioSource>();
        pageNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        sound.volume = PlayerPrefs.GetFloat("SFX");

        if (Input.GetKeyDown(KeyCode.N))
        {
            canvas.enabled = !canvas.enabled;
            if (canvas.enabled)
            {
                playerMovement.enabled = false;
                footsteps.Pause();
            }
            else
            {
                playerMovement.enabled = true;
                footsteps.UnPause();
            }
        }

        if (canvas.enabled && !isBusy)
        {
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

            text1.text = writtenEvents.Count > leftPage ? writtenEvents[leftPage] : "";
            text2.text = writtenEvents.Count > rightPage ? writtenEvents[rightPage] : "";

            
            if (events.Contains("archEnter"))
            {
                isBusy = true;
                StartCoroutine(Write("OH NO! I can't go out... Why did it take so long to enter by the way?"));
                events.Remove("archEnter");
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

    IEnumerator TurnPage(int n)
    {
        sound.clip = pageSound;
        sound.Play();

        yield return new WaitForSeconds(0.5f);

        sound.Stop();
        pageNumber += n;
        isBusy = false;
    }
}
