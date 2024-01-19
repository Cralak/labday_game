using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriterMachine : MonoBehaviour
{
    [SerializeField] Chess chessScript;
    [SerializeField, Range(0.1f, 1.0f)] float delay = 0.4f;

    string originalText;
    TextMeshProUGUI uiText;

    void Awake()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        originalText = uiText.text;
        uiText.text = null;
    }

    void Update()
    {
        if (chessScript != null && chessScript.isPlaying) uiText.text = null;
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

            uiText.text = originalText[..i];
            yield return new WaitForSeconds(delay);
        }
    }
}
