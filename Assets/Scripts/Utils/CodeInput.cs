using TMPro;
using UnityEngine;

public class CodeInput : MonoBehaviour
{
    Canvas canvas;
    TMP_Text displayed;
    string input = "";
    bool hasEnded = false;
    Diary diary;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        displayed = GetComponentInChildren<TMP_Text>();
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
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
            if (!diary.CheckEvent("firstFloorDoor") && input == KeyEvents.chessCode)
            {
                diary.AddEvent("firstFloorDoor");
                ChangePlayerState.Enable();
                UIState.isBusy = false;
                Cursor.lockState = CursorLockMode.Locked;
                canvas.enabled = false;
            }
            else if (!diary.CheckEvent("basementDoor") && input == KeyEvents.basementCode)
            {
                diary.AddEvent("basementDoor");
                ChangePlayerState.Enable();
                UIState.isBusy = false;
                Cursor.lockState = CursorLockMode.Locked;
                canvas.enabled = false;
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
