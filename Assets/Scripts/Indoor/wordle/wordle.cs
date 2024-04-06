using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using DG.Tweening;

public class Wordle : MonoBehaviour
{
    [SerializeField] TMP_FontAsset computerFont;
    [SerializeField] GameObject rules;
    [SerializeField] GameObject grid;

    readonly StreamReader reader = new("Assets/Scripts/indoor/wordle/validWords.txt");
    readonly StreamReader reader2 = new("Assets/Scripts/indoor/wordle/wordChoice.txt");
    List<string> validWords;
    List<string> wordChoice;

    public bool isPlaying = false;

    bool isTouching = false;
    bool isSwitching = false;
    GameObject player;
    GameObject playerCamera;
    GameObject flashlight;
    Vector3 initialRotation;
    Canvas UI;
    Diary diary;
    string mysteryWord;
    string guess = "";
    int attempts = 0;
    Color greenGuess;
    Color yellowGuess;
    Color wrongGuess;
    IEnumerator setLettersRed;

    void Start()
    {
        validWords = new List<string>(reader.ReadToEnd().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
        wordChoice = new List<string>(reader2.ReadToEnd().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
        reader.Close();
        reader2.Close();
        for (int i = 0; i < validWords.Count; i++)
        {
            validWords[i] = validWords[i][..5];
        }
        for (int i = 0; i < wordChoice.Count; i++)
        {
            wordChoice[i] = wordChoice[i][..5];
        }

        flashlight = GameObject.Find("Flashlight");
        playerCamera = flashlight.transform.parent.gameObject;
        player = playerCamera.transform.parent.gameObject;
        UI = GameObject.Find("UI").GetComponent<Canvas>();
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();

        greenGuess = new Color(0.0f, 0.79f, 0.0f);
        yellowGuess = new Color(1.0f, 0.79f, 0.0f);
        wrongGuess = Color.gray;
        setLettersRed = SetLettersRed();

        mysteryWord = RandomWord();
        diary.SetEventText("wordle", mysteryWord + "... Why is a word that terrifying the answer to this puzzle?");
    }

    void Update()
    {
        if (!isPlaying && isTouching && !isSwitching && !UIState.isBusy && KeyEvents.wordleCode == null && ToggleActions.IsPressed("interact"))
        {
            StartCoroutine(Play());
        }
        if (isPlaying) GameLoop();
    }

    IEnumerator Play()
    {
        isSwitching = true;
        initialRotation = playerCamera.transform.eulerAngles;
        UI.enabled = false;
        UIState.isBusy = true;
        ChangePlayerState.Disable();
        flashlight.SetActive(false);
        playerCamera.transform.DOMove(transform.position - transform.right * 0.6f, 2.0f);
        playerCamera.transform.DORotate(new Vector3(0.0f, transform.eulerAngles.y + 90.0f, 0.0f), 2.0f);


        yield return new WaitForSeconds(2.0f);

        isPlaying = true;
        Cursor.lockState = CursorLockMode.None;
        grid.SetActive(true);
        isSwitching = false;
    }

    public IEnumerator Unplay()
    {
        if (KeyEvents.wordleCode != null) yield return new WaitForSeconds(1.0f);

        isSwitching = true;
        isPlaying = false;
        grid.SetActive(false);
        rules.SetActive(false);
        playerCamera.transform.DOMove(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), 2);
        playerCamera.transform.DORotate(initialRotation, 2);

        yield return new WaitForSeconds(2.0f);

        UI.enabled = true;
        UIState.isBusy = false;
        ChangePlayerState.Enable();
        flashlight.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        isSwitching = false;
    }

    string RandomWord()
    {
        System.Random random = new();
        return wordChoice[random.Next(wordChoice.Count)];
    }

    bool CheckWord()
    {
        bool[] wordAvailabilities = { true, true, true, true, true };
        bool[] guessAvailabilities = { true, true, true, true, true };
        guess = guess.ToLower();

        if (guess == mysteryWord)
        {
            for (short i = 0; i < 5; i++)
            {
                GameObject.Find("square" + attempts + i).GetComponent<Image>().color = greenGuess;
            }
            return true;
        }

        for (short i = 0; i < 5; i++)
        {
            if (guess[i] == mysteryWord[i]) // Green
            {
                GameObject.Find("square" + attempts + i).GetComponent<Image>().color = greenGuess;
                wordAvailabilities[i] = false;
                guessAvailabilities[i] = false;
            }
        }

        for (short i = 0; i < 5; i++)
        {
            Image square = GameObject.Find("square" + attempts + i).GetComponent<Image>();
            if (mysteryWord.Contains(guess[i]))
            {
                if (guessAvailabilities[i])
                {
                    for (short j = 0; j < 5; j++)
                    {
                        if (wordAvailabilities[j] && guess[i] == mysteryWord[j]) // Yellow
                        {
                            square.color = yellowGuess;
                            wordAvailabilities[j] = false;
                            guessAvailabilities[i] = false;
                            break;
                        }
                    }

                    if (square.color == Color.white)
                    {
                        square.color = wrongGuess;
                    }
                }
            }
            else
            {
                square.color = wrongGuess;
            }
        }

        return false;
    }

    void OnGUI()
    {
        if (isPlaying && attempts < 6)
        {
            Event e = Event.current;
            if (e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1 && char.IsLetter(e.keyCode.ToString()[0]))
            {
                StopCoroutine(setLettersRed);
                for (int i = 0; i < 5; i++)
                {
                    GameObject.Find("square" + attempts + i).GetComponent<Image>().color = Color.white;
                }

                if (guess.Length < 5)
                {
                    string letter = e.keyCode.ToString();
                    guess += letter;
                    GameObject.Find("letter" + attempts + (guess.Length - 1)).GetComponent<TMP_Text>().text = letter;
                }
            }
            else if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Backspace)
            {
                StopCoroutine(setLettersRed);
                for (int i = 0; i < 5; i++)
                {
                    GameObject.Find("square" + attempts + i).GetComponent<Image>().color = Color.white;
                }

                if (guess.Length > 0)
                {
                    GameObject.Find("letter" + attempts + (guess.Length - 1)).GetComponent<TMP_Text>().text = "";
                    guess = guess.Substring(0, guess.Length - 1);
                }
            }
        }
    }

    IEnumerator SetLettersRed()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject.Find("square" + attempts + i).GetComponent<Image>().color = Color.red;
        }

        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < 5; i++)
        {
            GameObject.Find("square" + attempts + i).GetComponent<Image>().color = Color.white;
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < 5; i++)
        {
            GameObject.Find("square" + attempts + i).GetComponent<Image>().color = Color.red;
        }

        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < 5; i++)
        {
            GameObject.Find("square" + attempts + i).GetComponent<Image>().color = Color.white;
        }
    }

    IEnumerator ResetAndUnplay()
    {
        yield return new WaitForSeconds(1.0f);

        for (short i = 0; i < 6; i++)
        {
            for (short j = 0; j < 5; j++)
            {
                GameObject square = GameObject.Find("square" + i + j);
                square.GetComponent<Image>().color = Color.white;
                square.GetComponentInChildren<TMP_Text>().text = "";
            }
        }

        StartCoroutine(Unplay());

        attempts = 0;
        mysteryWord = RandomWord();
        diary.SetEventText("wordle", mysteryWord + "... Why is a word that terrifying the answer to this puzzle?");
    }

    void GameLoop()
    {
        if (Input.GetKeyDown(KeyCode.Return) && guess.Length == 5 && attempts < 6)
        {
            if (validWords.Contains(guess.ToLower()))
            {
                if (CheckWord())
                {
                    KeyEvents.wordleCode = mysteryWord;
                    diary.AddEvent("wordle");
                    StartCoroutine(Unplay());
                }
                else
                {
                    guess = "";
                    attempts += 1;

                    if (attempts >= 6)
                    {
                        StartCoroutine(ResetAndUnplay());
                    }
                }
            }
            else
            {
                StopCoroutine(setLettersRed);
                setLettersRed = SetLettersRed();
                StartCoroutine(setLettersRed);
            }
        }
    }

    // Triggered when another collider enters the trigger zone
    void OnTriggerEnter(Collider collider)
    {
        isTouching = true;
    }

    // Triggered when another collider exits the trigger zone
    void OnTriggerExit(Collider collider)
    {
        isTouching = false;
    }
}
