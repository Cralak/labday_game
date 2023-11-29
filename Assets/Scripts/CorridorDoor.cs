using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorDoor : MonoBehaviour
{
    bool isTouching;
    
    // Start is called before the first frame update
    void Start()
    {
        isTouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching && Input.GetKeyDown("e"))
        {
            transform.position += new Vector3(1f, 0f, 0f);
        }
    }

    void OnTriggerEnter(Collider collision)    
    {
        print("touch");
        isTouching = true;
    }

    void OnTriggerExit(Collider collision)   
    {
        isTouching = false;
    }
}
