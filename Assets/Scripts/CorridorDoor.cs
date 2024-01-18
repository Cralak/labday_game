using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorDoor : MonoBehaviour
{
    bool isTouching;
    bool close;
    
    // Start is called before the first frame update
    void Start()
    {
        isTouching = false;
        close = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching && Input.GetKeyDown(KeyCode.E))
        {
            if (close)
            {
                transform.position += new Vector3(5.0f, 0.0f, 3.5f);
                close = !close;
            }
            else
            {
                transform.position += new Vector3(-5.0f, 0.0f, -3.5f);
                close = !close;
            }
        }
    }

    void OnTriggerEnter(Collider collision)    
    {
        isTouching = true;
    }

    void OnTriggerExit(Collider collision)   
    {
        isTouching = false;
    }
}
