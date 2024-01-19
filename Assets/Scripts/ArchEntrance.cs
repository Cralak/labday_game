using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArchEntrance : MonoBehaviour
{
    [SerializeField] Image blackScreen;
    [SerializeField] Canvas canvas;

    GameObject player;
    Diary diary;
    Canvas UI;
    bool isTouching;
    PlayerMovement playerMovement;
    Canvas text;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        diary = GameObject.Find("Diary").GetComponent<Diary>();
        UI = GameObject.Find("UI").GetComponent<Canvas>();
        playerMovement = player.GetComponent<PlayerMovement>();
        blackScreen = canvas.GetComponentInChildren<Image>();
        text = GetComponent<Canvas>();
        isTouching = false;
        text.enabled = false;
        Color c = blackScreen.color;
        c.a = 0;
        blackScreen.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouching && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.enabled = false;
            player.GetComponent<AudioSource>().Pause();
            UI.enabled = false;
            StartCoroutine(FakeScreen());
            isTouching = false;
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

    IEnumerator FakeScreen()
    {
        for (float i = 0; i <= 1; i += 0.005f)
        {
            Color c = blackScreen.color;
            c.a = i;
            blackScreen.color = c;
            yield return new WaitForSeconds(0.005f);
        }

        player.transform.position= new Vector3(22, 1, 50);

        for (float i = 1.0f; i >= 0; i -= 0.005f)
        {
            Color c = blackScreen.color;
            c.a = i;
            blackScreen.color = c;
            yield return new WaitForSeconds(0.005f);
        }
        diary.events.Add("archEnter");
        playerMovement.enabled = true;
        UI.enabled = true;
    }
}
