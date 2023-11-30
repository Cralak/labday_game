using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    Canvas text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collision)    
    {
        text.enabled = !text.enabled;
    }

    void OnTriggerExit(Collider collision)   
    {
        text.enabled = !text.enabled;
    }
}
