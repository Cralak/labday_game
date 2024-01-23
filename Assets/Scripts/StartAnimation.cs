using System.Collections;
using UnityEngine;
using DG.Tweening;

public class StartAnimation : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;
    bool isPlaying;

    void Start()
    {
        InitializeComponents();
        DOTween.defaultEaseType = Ease.Linear;
    }

    void Update()
    {
        CheckAnimationStatus();
    }

    void InitializeComponents()
    {
        // Initialize required components
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        isPlaying = false;
    }

    void CheckAnimationStatus()
    {
        if (isPlaying)
        {
            StartCoroutine(PlayAnimation());
            playerMovement.enabled = false;
            isPlaying = false;
        }
    }

    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(5);

        player.transform.DORotate(new Vector3(0, -14.8f, 0), 5);
        player.transform.DOMove(new Vector3(17.8f, 1.08f, -8.3f), 5);

        yield return new WaitForSeconds(5);

        player.transform.DORotate(new Vector3(0, 20.6f, 0), 5);
        player.transform.DOMove(new Vector3(20.1f, 1.08f, -2.4f), 5);

        yield return new WaitForSeconds(5);

        player.transform.DORotate(new Vector3(0, 56.1f, 0), 5);
        player.transform.DOMove(new Vector3(35.3f, 1.08f, 2.6f), 5);

        yield return new WaitForSeconds(5);

        player.transform.DORotate(new Vector3(0, -7.16f, 0), 5);
        player.transform.DOMove(new Vector3(25.4f, 1.08f, 33.7f), 5);

        yield return new WaitForSeconds(5);

        playerMovement.enabled = true;
    }

    void OnTriggerEnter()
    {
        isPlaying = true;
    }
}
