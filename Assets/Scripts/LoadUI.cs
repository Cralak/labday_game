using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUI : MonoBehaviour
{
    GameObject diary;
    void Awake()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    void Start()
    {
        diary = GameObject.Find("Diary");
        diary.GetComponent<Diary>().events.Add("start");

        DontDestroyOnLoad(GameObject.Find("EventSystem"));
        DontDestroyOnLoad(GameObject.Find("UI"));
        DontDestroyOnLoad(diary);
        DontDestroyOnLoad(GameObject.Find("Inventory"));
        DontDestroyOnLoad(GameObject.Find("Settings"));
    }
}
