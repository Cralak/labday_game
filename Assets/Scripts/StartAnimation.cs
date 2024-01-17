using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartAnimation : MonoBehaviour
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
        isPlaying = false;
        DOTween.defaultEaseType = Ease.Linear;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying == true)
        {
            StartCoroutine(PlayAnimation());
            playerMovement.enabled = false;
            isPlaying = false;
        }
    }

    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(5);

        player.transform.DORotate(new Vector3(0f, -14.8f, 0f), 5);
        player.transform.DOMove(new Vector3(17.8f, 1.08f, -8.3f), 5);

        yield return new WaitForSeconds(5);

        player.transform.DORotate(new Vector3(0f, 20.6f, 0f), 5);
        player.transform.DOMove(new Vector3(20.1f, 1.08f, -2.4f), 5);

        yield return new WaitForSeconds(5);

        player.transform.DORotate(new Vector3(0f, 56.1f, 0f), 5);
        player.transform.DOMove(new Vector3(35.3f, 1.08f, 2.6f), 5);

        yield return new WaitForSeconds(5);

        player.transform.DORotate(new Vector3(0f, -7.16f, 0f), 5);
        player.transform.DOMove(new Vector3(25.4f, 1.08f, 33.7f), 5);

        yield return new WaitForSeconds(5);

        playerMovement.enabled = true;

    }

    void OnTriggerEnter()
    {
        isPlaying = true;
        print("start");
    }
}
