using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;


public class Jumpscare : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] VideoPlayer videoPlayer;
    Canvas UI;
    AudioSource audioSource;
    AudioSource audioSource2;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        UI = GameObject.Find("UI").GetComponent<Canvas>();
        audioSource = GetComponent<AudioSource>();
        audioSource2 = gameObject.GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        videoPlayer.time = 0;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        audioSource.Play();
        yield return new WaitForSeconds(0.50f);
        audioSource2.Stop();

        UIState.isBusy = true;
        ChangePlayerState.Disable();
        Cursor.lockState = CursorLockMode.None;
        UI.enabled = false;

        for (int i = 0; i <= 3; i++)
        {   
            canvas.enabled = true;

            yield return new WaitForSeconds(0.1f);

            canvas.enabled = false;

            yield return new WaitForSeconds(0.1f);
        }
        canvas.enabled = true;

        yield return new WaitForSeconds(1f);

        canvas.enabled = false;

        UIState.isBusy = false;
        ChangePlayerState.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        UI.enabled = true;

        print("Video Terminée");
    }
}