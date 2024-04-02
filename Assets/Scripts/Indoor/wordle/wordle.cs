using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;
using System.Xml.Linq;
using System;
using TMPro;

public class Wordle : MonoBehaviour
{
    [SerializeField] TMP_FontAsset computerFont;

    StreamReader reader = new StreamReader("Assets/Scripts/indoor/wordle/validWords.txt");
    StreamReader reader2 = new StreamReader("Assets/Scripts/indoor/wordle/wordChoice.txt");
    List<string> validWords;
    List<string> wordChoice;

    string mysteryWord;
    string guess = "";
    bool wordFound = false;
    int attempts = 0;

    void Start()
    {
        validWords = new List<string>((reader.ReadToEnd()).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
        wordChoice = new List<string>((reader2.ReadToEnd()).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
        reader.Close();
        reader2.Close();
        mysteryWord = RandomWord().Substring(0, 5);
        CreateLetters();
    }

    void Update()
    {
        if (!wordFound) GameLoop();
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
                GameObject newObject = new GameObject();
                newObject.name = "letter" + w + l;
                newObject.transform.parent = gameObject.GetComponentInChildren<Canvas>().transform;
                newObject.AddComponent<CanvasRenderer>();
                RectTransform rectTransform = newObject.AddComponent<RectTransform>();
                rectTransform.localPosition = new Vector3(minX + l * (maxX - minX) / 4, maxY - w * (maxY - minY) / 5, 0.0f);
                rectTransform.localRotation = Quaternion.identity;
                rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                TMP_Text letterField = newObject.AddComponent<TextMeshProUGUI>();
                letterField.font = computerFont;
                letterField.fontSize = 0.0003f;
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
            return true;
        }

        for (short i = 0; i < 5; i++)
        {
            if (guess[i] == mysteryWord[i]) // Green
            {
                availabilities[i] = false;
            }
        }

        for (short i = 0; i < 5; i++)
        {
            if (mysteryWord == guess[i])
            {
                for (short j = 0; j < 5; j++)
                {
                    if (availabilities[j] && guess[i] == mysteryWord[j]) // Yellow
                    {
                        availabilities[j] = false;
                        break;
                    }
                }
            }
            else // White
            {
                // userWord[i] = Fore.LIGHTBLACK_EX + userWord[i];
            }
        }

        return false;
    }

    void OnGUI()
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

    void GameLoop()
    {
        if (attempts < 6)
        {
            if (guess.Length == 5 && Input.GetKeyDown(KeyCode.Return))
            {
                wordFound = checkWord();
                if (wordFound)
                {
                    print("win");
                }
                else
                {
                    guess = "";
                    attempts += 1;
                }
            }
        }
    }
}
