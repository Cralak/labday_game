using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadOutdoor : MonoBehaviour
{
    Image loadScreen;

    void Start()
    {
        loadScreen = GameObject.Find("LoadScreen").GetComponent<Image>();
    }

    public void LoadScene()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        for (float i = 0; i <= 1; i += 0.05f)
        {
            Color c = loadScreen.color;
            c.a = i;
            loadScreen.color = c;

            yield return new WaitForSeconds(0.005f);
        }

        SceneManager.LoadScene("OutdoorScene");
    }
}
