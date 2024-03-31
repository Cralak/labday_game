using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadOutdoor : MonoBehaviour
{
    Image loadScreen;

    void Start()
    {
        // Get the Image component of the loading screen
        loadScreen = GameObject.Find("LoadScreen").GetComponent<Image>();
    }

    public void LoadScene()
    {
        // Start the asynchronous loading process
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        // Fade in the loading screen
        for (float i = 0; i <= 1; i += 0.05f)
        {
            Color c = loadScreen.color;
            c.a = i;
            loadScreen.color = c;

            yield return new WaitForSeconds(0.005f);
        }

        // Load the outdoor scene
        SceneManager.LoadScene("OutdoorScene");
    }
}
