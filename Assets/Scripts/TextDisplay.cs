using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    public Transform target;
    [SerializeField] Transform player;
    Canvas text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Canvas>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
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
