using System.Collections.Generic;
using UnityEngine;

public class KeyEvents : MonoBehaviour
{
    static readonly List<string> events = new();

    static public string chessCode;
    static public bool wordle = false;

    static public void AddEvent(string eventName)
    {
        events.Add(eventName);
    }

    static public bool CheckEvent(string eventName)
    {
        return events.Contains(eventName);
    }
}
