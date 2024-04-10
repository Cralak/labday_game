using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;


public class Jumpscare : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject playerCam;
    [SerializeField] GameObject player;
    [SerializeField] AudioClip BGMClip;

    Canvas UI;
    AudioSource audioSource;
    //AudioSource audioSource2;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        UI = GameObject.Find("UI").GetComponent<Canvas>();
        audioSource = GetComponent<AudioSource>();
        //audioSource2 = gameObject.GetComponents<AudioSource>()[1];
    }

    void OnTriggerEnter()
    {
        videoPlayer.time = 0;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        UIState.isBusy = true;
        ChangePlayerState.Disable();
        Cursor.lockState = CursorLockMode.Locked;
        UI.enabled = false;

        playerCam.transform.DOLookAt(transform.position + Vector3.up, 0.5f);

        yield return new WaitForSeconds(0.5f);

        audioSource.Play();
        yield return new WaitForSeconds(0.50f);
        //audioSource2.Stop();

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

        Destroy(GameObject.Find("BGM"));

        GameObject music = new("BGM");
        music.AddComponent<SetSFXVolume>();

        AudioSource BGMSource = music.AddComponent<AudioSource>();
        BGMSource.clip = BGMClip;
        BGMSource.loop = true;
        BGMSource.Play();
        DontDestroyOnLoad(music);

        DOTween.KillAll();
        StartCoroutine(Teleport.GoTo(player, new Vector3(19.0f, 0.0f, 7f), "Indoor"));
    }
}