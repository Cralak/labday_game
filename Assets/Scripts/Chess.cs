using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Chess : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject camera;

    PlayerMovement playerMovement;
    bool isTouching;
    bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        isTouching = false;
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching && Input.GetKeyDown("e"))
        {
            if (isPlaying)
            {
                StartCoroutine(Unplay());
            }
            else
            {
                isPlaying = true;
                playerMovement.enabled = false;
                camera.transform.DOMove(new Vector3(-12.31f, 1.75f, 12.7f ), 2);
                camera.transform.DORotate(new Vector3(90f, 0f, 0f ), 2);
            }
        }
    }

    void OnTriggerEnter(Collider collider)    
    {
        if (collider == player.GetComponent<CharacterController>())
        {
            isTouching = true;
        }
    }

    void OnTriggerExit(Collider collider)    
    {
        if (collider == player.GetComponent<CharacterController>())
        {
            isTouching = false;
        }
    }

    IEnumerator Unplay()
    {
        isPlaying = false;
        camera.transform.DOMove(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), 2);
        camera.transform.DORotate(new Vector3(0f, 0f, 0f ), 2);

        yield return new WaitForSeconds(2f);

        playerMovement.enabled = true;
    }
}
