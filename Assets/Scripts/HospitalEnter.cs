using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HospitalEnter : MonoBehaviour
{
    [SerializeField] GameObject key;

    GameObject player;
    PlayerMovement playerMovement;
    Diary diary;
    bool isTouching;
    Canvas text;
    Inventory inventoryScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        diary = GameObject.Find("Diary").GetComponent<Diary>();
        inventoryScript = player.GetComponent<Inventory>();
        isTouching = false;
        text = GetComponent<Canvas>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching == true && inventoryScript.inventory.Contains(key) && Input.GetKeyDown("e"))
        {
            StartCoroutine(LoadHospital());
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

    IEnumerator LoadHospital()
    {
        playerMovement.enabled = false;
        player.transform.position = new Vector3(4.0f, 1.0f, 2.0f);
        inventoryScript.inventory.Remove(key);

        yield return new WaitForSeconds(0.1f);

        diary.events.Add("indoor");
        playerMovement.enabled = true;
        SceneManager.LoadScene("IndoorScene");
    }
}
