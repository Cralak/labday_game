using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;
using System.Xml.Linq;
using System;

public class validWords : MonoBehaviour
{
    StreamReader reader = new StreamReader("Assets/Scripts//wordle/validWords.txt");
    StreamReader reader2 = new StreamReader("Assets/Scripts//wordle/wordChoice.txt");
    private static List<string> validWordsList;
    private static List<string> wordChoice;

    void Start(){
        validWordsList = new List<string>((reader.ReadToEnd()).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
        wordChoice = new List<string>((reader2.ReadToEnd()).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));

        Debug.Log(wordChoice[1]);
    }
}
