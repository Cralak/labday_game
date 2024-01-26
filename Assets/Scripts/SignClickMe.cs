using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignClickMe : MonoBehaviour
{
    bool isTouching;
    bool isDisplay;
    // Start is called before the first frame update
    void Start()
    {
        isTouching = false;
        isDisplay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching == true && Input.GetMouseButtonDown(0))
        {
            isDisplay = true;
            print("sign display");
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
}
