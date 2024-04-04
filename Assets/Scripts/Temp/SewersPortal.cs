using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewersPortal : MonoBehaviour
{
    bool first = false;
    bool second = false;
    bool third = false;
    bool isTouching = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                first = true;
                second = false;
                third = false;
                print("1oke");
            }
            else if (Input.GetKeyDown(KeyCode.I) && first)
            {
                first = false;
                second = true;
                print("2ok");
            }
            else if (Input.GetKeyDown(KeyCode.O) && second)
            {
                second = false;
                third = true;
                print("3ok");
            }
            else if (Input.GetKeyDown(KeyCode.U) && third)
            {
                transform.position += new Vector3(0f, 0.1f, 0f);
                third = false;
                print("gagné");
            }
            // Si une autre touche est enfoncée sans respecter l'ordre, réinitialise les variables
            else if (Input.anyKeyDown)
            {
                first = false;
                second = false;
                third = false;
                print("perdu");
            }
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
