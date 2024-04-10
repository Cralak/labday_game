using System.Collections;
using TMPro;
using UnityEngine;

public class WriterMachine : MonoBehaviour
{
    [SerializeField] Chess chessScript;
    [SerializeField, Range(0.1f, 1.0f)] float delay = 0.4f;

    string originalText;
    TextMeshProUGUI uiText;
    IEnumerator coroutine;

    void Start()
    {
        InitializeComponents();
        coroutine = LetterByLetter();
    }

    void Update()
    {
        // Check if chessScript is playing and reset the text
        if (chessScript.isPlaying)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    void OnTriggerEnter()
    {
        // Start the coroutine when triggered
        StartCoroutine(coroutine);
    }

    void OnTriggerExit()
    {
        // Clear the text when exiting the trigger
        uiText.text = null;
    }

    void InitializeComponents()
    {
        // Initialize required components
        uiText = GetComponent<TextMeshProUGUI>();
        originalText = uiText.text;
        uiText.text = null;
    }

    IEnumerator LetterByLetter()
    {
        // Display text letter by letter with a delay
        for (int i = 0; i <= originalText.Length; i++)
        {
            // Check if chessScript is playing and exit the coroutine
            uiText.text = originalText[..i];
            yield return new WaitForSeconds(delay);
        }
    }
}
