using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenInput : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] TMP_InputField inputField;

    string answer;
    
    GameObject player;
     
    bool isTouching = false;
    bool isTyping = false;


    // Start is called before the first frame update
    void Start()
    {
        isTouching = false;
        canvas.enabled = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching && ToggleActions.IsPressed("interact") && !isTyping)
        {
            isTyping = true;
            print("touche et touche");
            ChangePlayerState.Disable();
            canvas.enabled = !canvas.enabled;
            Cursor.lockState = CursorLockMode.None;
            UIState.isBusy = true;
        }
    }

    void OnTriggerEnter()
    {
        isTouching = true;
        print("touche");
    }
    void OnTriggerExit()
    {
        isTouching = false;
        print("touche plus");
    }

    public void Check()
    {
        answer = inputField.text;
        if (answer == "tst")
        {
            print("ça marche");
        }
    }

    public void Exit()
    {
        isTyping = false;
        print("touche et touche");
        ChangePlayerState.Enable();
        canvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        UIState.isBusy = false;
    }
}
