using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LockedWithKey : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject key;

    AudioSource doorNoise;
    bool isColliding;
    Inventory inventoryScript;

    // Start is called before the first frame update
    void Start()
    {
        isColliding = false;
        inventoryScript = player.GetComponent<Inventory>();
        doorNoise = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        doorNoise.volume = PlayerPrefs.GetFloat("SFX");
        if (isColliding && Input.GetKeyDown("e") && inventoryScript.inventory.Contains(key))
        {
            inventoryScript.inventory.Remove(key);
            doorNoise.Play();
            transform.DOMove(transform.position + new Vector3(5f, 0f, 3.5f), 5f);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        isColliding = true;
    }

    void OnTriggerExit(Collider collision)
    {
        isColliding = false;
    }
}
