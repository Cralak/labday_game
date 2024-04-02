using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;
using System.Xml.Linq;
using System;
using TMPro;
using DG.Tweening;

public class Wordle : MonoBehaviour
{
    [SerializeField] TMP_FontAsset computerFont;

    StreamReader reader = new StreamReader("Assets/Scripts/indoor/wordle/validWords.txt");
    StreamReader reader2 = new StreamReader("Assets/Scripts/indoor/wordle/wordChoice.txt");
    List<string> validWords;
    List<string> wordChoice;
    bool isTouching = false;
    bool isSwitching = false;
    bool isPlaying = false;
    GameObject player;
    GameObject playerCamera;
    GameObject flashlight;
    Vector3 initialRotation;
    string mysteryWord;
    string guess = "";
    int attempts = 0;
    Color greenGuess;
    Color yellowGuess;

    void Start()
    {
        
        validWords = new List<string>(reader.ReadToEnd().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
        wordChoice = new List<string>(reader2.ReadToEnd().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
        reader.Close();
        reader2.Close();

        for (int i = 0; i < validWords.Count; i++)
        {
            validWords[i] = validWords[i].Substring(0, 5);
        }
        for (int i = 0; i < wordChoice.Count; i++)
        {
            wordChoice[i] = wordChoice[i].Substring(0, 5);
        }
        flashlight = GameObject.Find("Flashlight");
        playerCamera = flashlight.transform.parent.gameObject;
        player = playerCamera.transform.parent.gameObject;
        greenGuess = new Color(0.0f, 0.79f, 0.0f);
        yellowGuess = new Color(1.0f, 0.79f, 0.0f);
        mysteryWord = RandomWord();
        print(mysteryWord);
        CreateLetters();
    }

    void Update()
    {
        if (!isPlaying && isTouching && !isSwitching && !UIState.isBusy && !KeyEvents.wordle && ToggleActions.IsPressed("interact"))
        {
            StartCoroutine(Play());
        }
        if (isPlaying) GameLoop();
    }

    IEnumerator Play()
    {
        isSwitching = true;
        initialRotation = playerCamera.transform.eulerAngles;
        UIState.isBusy = true;
        ChangePlayerState.Disable();
        flashlight.SetActive(false);
        playerCamera.transform.DOMove(transform.position + new Vector3(-0.6f, 0.0f, 0.0f), 2.0f);
        playerCamera.transform.DORotate(new Vector3(0.0f, transform.eulerAngles.y + 90.0f, 0.0f), 2.0f);


        yield return new WaitForSeconds(2.0f);

        isPlaying = true;
        isSwitching = false;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator Unplay()
    {
        if (isPlaying)
        {
            isSwitching = true;
            isPlaying = false;
            playerCamera.transform.DOMove(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), 2);
            playerCamera.transform.DORotate(initialRotation, 2);

            yield return new WaitForSeconds(2.0f);

            UIState.isBusy = false;
            ChangePlayerState.Enable();
            flashlight.SetActive(true);
            isSwitching = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Quit()
    {
        StartCoroutine(Unplay());
    }

    void CreateLetters()
    {
        float minX = -0.001f;
        float maxX = 0.001f;
        float minY = -0.0008f;
        float maxY = 0.001f;

        for (int w = 0; w < 6; w++)
        {
            for (int l = 0; l < 5; l++)
            {
                GameObject background = new GameObject();
                background.name = "square" + w + l;
                background.transform.parent = gameObject.GetComponentInChildren<Canvas>().transform;

                RectTransform backgroundTransform = background.AddComponent<RectTransform>();
                backgroundTransform.localPosition = new Vector3(minX + l * (maxX - minX) / 4, maxY - w * (maxY - minY) / 5, 0.0f);
                backgroundTransform.sizeDelta = new Vector2(0.0002f, 0.0002f);
                backgroundTransform.localRotation = Quaternion.identity;
                backgroundTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                background.AddComponent<CanvasRenderer>();
                Image image = background.AddComponent<Image>();


                GameObject letter = new GameObject();
                letter.name = "letter" + w + l;
                letter.transform.parent = background.transform;

                RectTransform letterTransform = letter.AddComponent<RectTransform>();
                letterTransform.localPosition = new Vector3(-0.000042f, 0.00002f, 0.0f);
                letterTransform.localRotation = Quaternion.identity;
                letterTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                letter.AddComponent<CanvasRenderer>();
                TMP_Text letterField = letter.AddComponent<TextMeshProUGUI>();
                letterField.font = computerFont;
                letterField.fontSize = 0.00025f;
                letterField.color = Color.black;
                letterField.alignment = TextAlignmentOptions.Center;
            }
        }
    }

    string RandomWord()
    {
        System.Random random = new System.Random();
        return wordChoice[random.Next(wordChoice.Count)];
    }

    bool checkWord()
    {
        List<bool> availabilities = new() { true, true, true, true, true };
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
                availabilities[i] = false;
            }
        }

        for (short i = 0; i < 5; i++)
        {
            if (mysteryWord.Contains(guess[i]))
            {
                for (short j = 0; j < 5; j++)
                {
                    if (availabilities[j] && guess[i] == mysteryWord[j]) // Yellow
                    {
                        GameObject.Find("square" + attempts + i).GetComponent<Image>().color = yellowGuess;
                        availabilities[j] = false;
                        break;
                    }
                }
            }
        }

        return false;
    }

    void OnGUI()
    {
        if (isPlaying)
        {
            Event e = Event.current;
            if (e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1 && char.IsLetter(e.keyCode.ToString()[0]))
            {
                if (guess.Length < 5)
                {
                    string letter = e.keyCode.ToString();
                    guess += letter;
                    GameObject.Find("letter" + attempts + (guess.Length - 1)).GetComponent<TMP_Text>().text = letter;
                }
            }
            else if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Backspace)
            {
                if (guess.Length > 0)
                {
                    GameObject.Find("letter" + attempts + (guess.Length - 1)).GetComponent<TMP_Text>().text = "";
                    guess = guess.Substring(0, guess.Length - 1);
                }
            }
        }
    }

    void GameLoop()
    {
        if (attempts < 6 && guess.Length == 5 && Input.GetKeyDown(KeyCode.Return))
        {
            if (validWords.Contains(guess.ToLower()))
            {
                if (checkWord())
                {
                    StartCoroutine(Unplay());
                    KeyEvents.wordle = true;
                }
                else
                {
                    guess = "";
                    attempts += 1;
                }
            }
            else
            {
                print("word not exists");
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
