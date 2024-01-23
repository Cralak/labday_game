using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUI : MonoBehaviour
{
    GameObject diary;

    void Awake()
    {
        // Load the UI scene in additive mode
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    void Start()
    {
        // Find the Diary GameObject in the scene
        diary = GameObject.Find("Diary");

        // Add the "start" event to the Diary's events list
        diary.GetComponent<Diary>().events.Add("start");

        // Prevent specified GameObjects from being destroyed on scene changes
        DontDestroyOnLoad(GameObject.Find("EventSystem"));
        DontDestroyOnLoad(GameObject.Find("UI"));
        DontDestroyOnLoad(diary);
        DontDestroyOnLoad(GameObject.Find("Inventory"));
        DontDestroyOnLoad(GameObject.Find("Settings"));
    }
}
