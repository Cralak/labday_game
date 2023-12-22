using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Chess : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject mainCamera;

    Camera componentCamera;
    PlayerMovement playerMovement;
    bool isTouching;
    bool isPlaying;
    bool isSwitching;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        isTouching = false;
        isPlaying = false;
        isSwitching = false;
        componentCamera = mainCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            if (isTouching && !isSwitching && Input.GetKeyDown("e"))
            {
                StartCoroutine(Play());
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(componentCamera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    print(hit.point);
                }
            }

            if (isTouching && !isSwitching && Input.GetKeyDown("e"))
            {
                StartCoroutine(Unplay());
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
        mainCamera.transform.DOMove(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), 2);
        mainCamera.transform.DORotate(new Vector3(0f, 0f, 0f ), 2);
        isSwitching = true;

        yield return new WaitForSeconds(2f);

        isSwitching = false;
        playerMovement.enabled = true;
        Cursor.lockState = CursorLockMode.Locked; 

    }

    IEnumerator Play()
    {
        isPlaying = true;
        playerMovement.enabled = false;
        mainCamera.transform.DOMove(new Vector3(-12.31f, 1.75f, 12.7f ), 2);
        mainCamera.transform.DORotate(new Vector3(90f, 0f, 0f ), 2);
        isSwitching = true;

        yield return new WaitForSeconds(2f);

        isSwitching = false;
        Cursor.lockState = CursorLockMode.None;

    }
}
