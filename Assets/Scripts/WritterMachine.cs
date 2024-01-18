using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriterMachine : MonoBehaviour
{
    [SerializeField, Range(0.1f, 1f)] float delay = 0.4f;

    string originalText;
    TextMeshProUGUI uiText;
    Chess chessScript;

    void Awake()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        originalText = uiText.text;
        uiText.text = null;
        GameObject pressToPlayObject = GameObject.Find("PressToPlay");
        chessScript = pressToPlayObject.GetComponent<Chess>();
    }

    void Update()
    {
        if (chessScript != null && chessScript.isPlaying)
        {
            uiText.text = null;
        }
    }

    void OnTriggerEnter()
    {
        StartCoroutine(LetterByLetter());
    }

    void OnTriggerExit()
    {
        uiText.text = null;
    }

    IEnumerator LetterByLetter()
    {
        for (int i = 0; i <= originalText.Length; i++)
        {
            if (chessScript != null && chessScript.isPlaying)
            {
                uiText.text = null;
                yield break;
            }

            uiText.text = originalText.Substring(0, i);
            yield return new WaitForSeconds(delay);
        }
    }
}
