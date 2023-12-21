using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WriterMachine : MonoBehaviour
{
    string originalText;
    TextMeshProUGUI uiText;
    public float delay = 0.4f;

    void Awake()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        originalText = uiText.text;
        uiText.text = null; 
    }
    void OnTriggerEnter()
    {
        StartCoroutine(LetterByLetter());
    }


    IEnumerator LetterByLetter()
    {
        for (int i = 0; i <= originalText.Length; i++) 
        {
            uiText.text = originalText.Substring(0, i);
            yield return new WaitForSeconds(delay);
        }
    }
}

