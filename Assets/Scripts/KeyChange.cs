using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChange : MonoBehaviour
{
    bool pushed;

    // Start is called before the first frame update
    void Start()
    {
        pushed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pushed && Input.anyKeyDown)
        {
            string a = Input.inputString;
            pushed = false;
            print((KeyCode)Enum.Parse(typeof(KeyCode), a) == KeyCode.A);
        }
    }

    public void ChangeKey()
    {
        pushed = true;
    }
}
