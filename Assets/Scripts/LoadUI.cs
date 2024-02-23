using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUI : MonoBehaviour
{
    void Awake()
    {
        // Load the UI scene in additive mode
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    void Start()
    {
        // Find the UI GameObject in the scene
        GameObject UI = GameObject.Find("UI");

        // Activate UI's display canvas
        UI.GetComponent<Canvas>().enabled = true;

        UIState.isBusy = false;

        // Prevent specified GameObjects from being destroyed on scene changes
        foreach (GameObject thing in UI.scene.GetRootGameObjects()) DontDestroyOnLoad(thing);
    }
}
