using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUI : MonoBehaviour
{
    GameObject UI;

    void Awake()
    {
        // Load the UI scene in additive mode
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    void Start()
    {
        // Find the UI GameObject in the scene
        UI = GameObject.Find("UI");

        // Activate UI's display canvas
        UI.GetComponent<Canvas>().enabled = true;

        // Prevent specified GameObjects from being destroyed on scene changes
        DontDestroyOnLoad(GameObject.Find("EventSystem"));
        DontDestroyOnLoad(GameObject.Find("Settings"));
        DontDestroyOnLoad(GameObject.Find("OpenedDiary"));
        DontDestroyOnLoad(GameObject.Find("Inventory"));
        DontDestroyOnLoad(UI);
    }
}
