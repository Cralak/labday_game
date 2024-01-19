using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HospitalEnter : MonoBehaviour
{
    [SerializeField] GameObject key;

    GameObject player;
    bool isTouching;
    Canvas text;
    Inventory inventoryScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        inventoryScript = player.GetComponent<Inventory>();
        isTouching = false;
        text = GetComponent<Canvas>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouching == true && inventoryScript.inventory.Contains(key) && Input.GetKeyDown("e"))
        {
            SceneManager.LoadScene("IndoorScene");
            inventoryScript.inventory.Remove(key);
        }
    }

    void OnTriggerEnter()
    {
        isTouching = true;
        text.enabled = true;
    }

    void OnTriggerExit()
    {
        isTouching = false;
        text.enabled = false;
    }
}
