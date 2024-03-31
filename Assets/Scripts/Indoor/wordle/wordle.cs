using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;
using System.Xml.Linq;
using System;
using TMPro;

public class validWords : MonoBehaviour
{
    StreamReader reader = new StreamReader("Assets/Scripts//wordle/validWords.txt");
    StreamReader reader2 = new StreamReader("Assets/Scripts//wordle/wordChoice.txt");
    private static List<string> validWordsList;
    private static List<string> wordChoice;

    public TextMeshPro tmp;

    void Start(){
        validWordsList = new List<string>((reader.ReadToEnd()).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
        wordChoice = new List<string>((reader2.ReadToEnd()).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
    }

    public static string randomWord(){
        System.Random random = new System.Random();
        return validWordsList[random.Next(validWordsList.Count)];
    }

    public static void gameLoop(string word){
        bool wordFound = false;
        int attempts = 6;
        while(!wordFound && attempts>0){

        }
    }
}
