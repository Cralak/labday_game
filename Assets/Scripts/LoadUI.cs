using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUI : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("EventSystem"));
        DontDestroyOnLoad(GameObject.Find("UI"));
        DontDestroyOnLoad(GameObject.Find("Diary"));
        DontDestroyOnLoad(GameObject.Find("Inventory"));
        DontDestroyOnLoad(GameObject.Find("Settings"));
    }
}
