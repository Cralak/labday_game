using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OpenDoorWithKey : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject key;
    [SerializeField] Diary diary;

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
            transform.DOMove(transform.position + new Vector3(5f, 0f, 3.5f), 5f);
            doorNoise.Play();
            diary.events.Add("rustyKey");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == player) isColliding = true;
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == player) isColliding = false;
    }
}
