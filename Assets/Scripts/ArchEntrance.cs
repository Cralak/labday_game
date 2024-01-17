using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArchEntrance : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    Image blackScreen;
    [SerializeField]
    Canvas canvas;
    bool isTouching;
    PlayerMovement playerMovement;
    Canvas text;

    // Start is called before the first frame update
    void Start()
    {
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
        if(isTouching && Input.GetKeyDown("e"))
        {
            playerMovement.enabled = false;
            StartCoroutine(fakeScreen());
            print("Oke");
        }
    }

    void OnTriggerEnter()
    {
        isTouching = true;
        text.enabled = true;
        print("ArchTouch");
    }

    void OnTriggerExit()
    {
        isTouching = false;
        text.enabled = false;
    }

    IEnumerator fakeScreen()
    {
        for (float i = 0; i <= 1; i += 0.005f)
        {
            Color c = blackScreen.color;
            c.a = i;
            blackScreen.color = c;
            yield return new WaitForSeconds(0.005f);
        }

        player.transform.position= new Vector3(22, 1, 50);

        for (float i = 1f; i >= 0; i -= 0.005f)
        {
            Color c = blackScreen.color;
            c.a = i;
            blackScreen.color = c;
            yield return new WaitForSeconds(0.005f);
        }
        playerMovement.enabled = true;
    }
}
