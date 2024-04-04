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
            if (Input.GetKeyDown("h"))
            {
                first = true;
                print("1oke");
            }
            if (Input.GetKeyDown("i") && first)
            {
                second = true;
                print("2ok");
            }
            if (Input.GetKeyDown("o") && second)
            {
                third = true;
                print("3ok");
            }
            if (Input.GetKeyDown("u") && third)
            {
                transform.position += new Vector3(0f, 0.1f, 0f);
                first = false;
                second = false;
                third = false;
                print("gagné");
            }
            // Si une autre touche est enfoncée sans respecter l'ordre, réinitialise les variables
            if (Input.anyKeyDown && !(Input.GetKeyDown("h") && first || Input.GetKeyDown("i") && second || Input.GetKeyDown("o") && third))
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
