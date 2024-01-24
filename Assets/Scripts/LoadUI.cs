using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUI : MonoBehaviour
{
    GameObject UI;
    GameObject diary;

    void Awake()
    {
        // Load the UI scene in additive mode
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    void Start()
    {
        // Find the Diary GameObject in the scene
        UI = GameObject.Find("UI");
        diary = GameObject.Find("Diary");

        // Add the "start" event to the Diary's events list
        diary.GetComponent<Diary>().events.Add("start");

        // Activate UI's display canvas
        UI.GetComponent<Canvas>().enabled = true;

        // Prevent specified GameObjects from being destroyed on scene changes
        DontDestroyOnLoad(GameObject.Find("EventSystem"));
        DontDestroyOnLoad(UI);
        DontDestroyOnLoad(diary);
        DontDestroyOnLoad(GameObject.Find("Inventory"));
        DontDestroyOnLoad(GameObject.Find("Settings"));
    }
}
