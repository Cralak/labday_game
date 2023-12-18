using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    [SerializeField] CharacterController player;
    bool isColliding = false;


    void OnTriggerEnter(Collider collision)
    {
        if (collision == player){
            isColliding = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision == player)
        {
            isColliding = false;
        }
    }

    void Update()
    {
        if (isColliding && Input.GetKeyDown("e"))
        {
            print("Ludwig, trop beau");
            //Destroy(gameObject);
        }
    }
}
