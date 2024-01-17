using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedWithKey : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject key;

    bool isTouching;
    Inventory inventoryScript;
    
    // Start is called before the first frame update
    void Start()
    {
        isTouching = false;
        inventoryScript = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching && Input.GetKeyDown("e"))
        {
            inventoryScript.inventory.Remove(key);
            transform.position += new Vector3(5f, 0f, 3.5f);
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
