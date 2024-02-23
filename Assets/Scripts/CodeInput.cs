using TMPro;
using UnityEngine;

public class CodeInput : MonoBehaviour
{
    Canvas canvas;
    TMP_Text displayed;
    string input;
    bool hasEnded;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        displayed = GetComponentInChildren<TMP_Text>();
        input = "";
        hasEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        string text = "";
        for (int i = 0; i < input.Length; i++)
        {
            text += input[i] + " ";
        }
        for (int i = input.Length; i < 5; i++)
        {
            text += "_ ";
        }
        displayed.text = text[..9];

        if (hasEnded && canvas.enabled)
        {
            hasEnded = false;
            input = "";
        }

        if (!canvas.enabled)
        {
            hasEnded = true;
        }

        if (input.Length == 5)
        {
            if (input == KeyEvents.chessCode)
            {
                KeyEvents.AddEvent("digicodeDoor");
                canvas.enabled = false;
                UIState.isBusy = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                input = "";
            }
        }
    }

    public void AddDigit(string digit)
    {
        input += digit;
    }
}
