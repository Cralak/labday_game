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
    private List<string> validWordsList;
    private List<string> wordChoice;

    public TMP_Text tmp;

    void Start()
    {
        validWordsList = new List<string>((reader.ReadToEnd()).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
        wordChoice = new List<string>((reader2.ReadToEnd()).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));

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
                newObject.transform.parent = gameObject.transform;
                newObject.AddComponent<CanvasRenderer>();
                RectTransform rectTransform = newObject.AddComponent<RectTransform>();
                rectTransform.localPosition = new Vector3(minX + l * (maxX - minX) / 4, maxY - w * (maxY - minY) / 5, 0.0f);
                rectTransform.localRotation = Quaternion.identity;
                rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                TMP_Text letterField = newObject.AddComponent<TextMeshProUGUI>();
                letterField.font = computerFont;
                letterField.fontSize = 0.0003f;
                letterField.text = "A";
                letterField.alignment = TextAlignmentOptions.Center;
            }
        }
    }

    string RandomWord()
    {
        System.Random random = new System.Random();
        return validWordsList[random.Next(validWordsList.Count)];
    }

    bool checkWord(string userWord, string mysteryWord)
    {
        List<bool> availabilities = new() { true, true, true, true, true };

        if (userWord == mysteryWord)
        {
            return true;
        }

        for (short i = 0; i < 5; i++)
        {
            if (userWord[i] == mysteryWord[i]) // Green
            {
                availabilities[i] = false;
            }
        }

        for (short i = 0; i < 5; i++)
        {
            if (mysteryWord.Contains(userWord[i]))
            {
                for (short j = 0; j < 5; j++)
                {
                    if (availabilities[j] && userWord[i] == mysteryWord[j]) // Yellow
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

    void GameLoop(string word)
    {
        bool wordFound = false;
        short attempts = 6;
        while (!wordFound && attempts > 0)
        {






        }
    }
}
