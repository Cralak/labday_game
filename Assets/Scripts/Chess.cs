using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;
using UnityEditor;

public class Chess : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject mainCamera;
    [SerializeField]
    GameObject table;
    [SerializeField]
    GameObject pieceWhite1;
    [SerializeField]
    GameObject pieceWhite2;
    [SerializeField]
    GameObject pieceWhite3;
    [SerializeField]
    GameObject pieceBlack1;
    [SerializeField]
    GameObject pieceBlack2;
    [SerializeField]
    GameObject pieceBlack3;
    [SerializeField]
    GameObject pieceBlack4;
    Camera componentCamera;
    PlayerMovement playerMovement;
    bool isTouching;
    bool isPlaying;
    bool isSwitching;
    GameObject square;
    String lastClicked;
    bool firstMoveDone;
    bool secondMoveDone;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        componentCamera = mainCamera.GetComponent<Camera>();
        isTouching = false;
        isPlaying = false;
        isSwitching = false;
        lastClicked = "64";
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
            StartCoroutine(Puzzle());

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
        mainCamera.transform.DORotate(new Vector3(0f, 0f, 0f), 2);
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
        mainCamera.transform.DOMove(new Vector3(-12.31f, 1.75f, 12.7f), 2);
        mainCamera.transform.DORotate(new Vector3(90f, 0f, 0f), 2);
        isSwitching = true;

        yield return new WaitForSeconds(2f);

        isSwitching = false;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator Puzzle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(componentCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.name.Contains("Black") || hit.transform.name.Contains("White"))
                {
                    RaycastHit hit2;
                    Physics.Raycast(hit.transform.position, -Vector3.up, out hit2);
                    square = hit2.transform.gameObject;
                }
                else
                {
                    square = hit.transform.gameObject;
                }

                if (!firstMoveDone)
                {
                    if (lastClicked == "19" && square.name == "17")
                    {
                        pieceWhite1.transform.DOMoveX(-12.537f, 2f);

                        yield return new WaitForSeconds(2f);

                        pieceBlack1.transform.DOMoveX(-12.641f, 2f);

                        yield return new WaitForSeconds(2f);

                        firstMoveDone = true;
                    }
                }
                else if (!secondMoveDone)
                {
                    if (lastClicked == "24" && square.name == "42")
                    {
                        pieceWhite2.transform.DOMove(new Vector3(-12.45f, 1.3315f, 12.826f), 2f);

                        yield return new WaitForSeconds(1.6f);

                        pieceBlack2.GetComponent<Renderer>().enabled = false;

                        yield return new WaitForSeconds(0.4f);

                        pieceBlack3.transform.DOMove(new Vector3(-12.45f, 1.3315f, 12.826f), 2f);

                        yield return new WaitForSeconds(1f);

                        pieceWhite2.GetComponent<Renderer>().enabled = false;

                        yield return new WaitForSeconds(1f);

                        secondMoveDone = true;
                    }
                }
                else
                {
                    if (lastClicked == "3" && square.name == "59")
                    {
                        pieceWhite3.transform.DOMoveZ(13.022f, 2f);

                        yield return new WaitForSeconds(1.7f);

                        pieceBlack4.GetComponent<Renderer>().enabled = false;

                        yield return new WaitForSeconds(0.3f);

                        StartCoroutine(Unplay());
                    }
                }
                lastClicked = square.name;
            }
        }
    }
}
