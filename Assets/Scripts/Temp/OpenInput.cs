using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenInput : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Canvas text;
    [SerializeField] TMP_InputField inputField;

    string answer;
    
    GameObject player;
     
    bool isTouching = false;
    bool isTyping = false;
    bool isResolved = false;


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
        if (isTouching && ToggleActions.IsPressed("interact") && !isTyping && !isResolved)
        {
            isTyping = true;
            ChangePlayerState.Disable();
            canvas.enabled = !canvas.enabled;
            Cursor.lockState = CursorLockMode.None;
            UIState.isBusy = true;
        }
    }

    void OnTriggerEnter()
    {
        isTouching = true;
    }
    void OnTriggerExit()
    {
        isTouching = false;
    }

    public void Check()
    {
        answer = inputField.text;
        if (answer == KeyEvents.wordleCode)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,130,0));
            isResolved = true;
            text.enabled = false;
            Exit();
        }
    }

    public void Exit()
    {
        isTyping = false;
        ChangePlayerState.Enable();
        canvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        UIState.isBusy = false;
    }
}
