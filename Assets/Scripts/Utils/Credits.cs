using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        GameObject sacrifice = new("sacrifice");
        DontDestroyOnLoad(sacrifice);

        foreach (GameObject thing in sacrifice.scene.GetRootGameObjects()) Destroy(thing);

        StartCoroutine(DisplayCredits());
    }

    IEnumerator DisplayCredits()
    {
        transform.DOMoveY(10.8f * Screen.height, 60.0f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(63.0f);

        SceneManager.LoadScene("MainMenu");
    }
}
