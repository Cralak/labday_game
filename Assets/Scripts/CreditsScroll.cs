using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayCredits());
    }

    IEnumerator DisplayCredits()
    {
        transform.DOMoveY(11700.0f, 60.0f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(63.0f);

        SceneManager.LoadScene("MainMenu");
    }
}
