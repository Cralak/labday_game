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
        if (isTouching && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("indoorScene");
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        isTouching = true;
    }

    void OnTriggerExit(Collider collider)
    {
        isTouching = false;
    }
}
