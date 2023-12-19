using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoIndoor : MonoBehaviour
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
            print("e press");
            SceneManager.LoadScene("indoorScene");
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
