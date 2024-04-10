using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class GhostLady : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Transform player;
    bool isFollowing;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        StartCoroutine(GoToStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing) navMeshAgent.destination = player.position;
    }

    IEnumerator GoToStart()
    {
        yield return new WaitForSeconds(5.0f);

        transform.DORotate(new Vector3(0.0f, 180.0f, 0.0f), 1.0f);

        yield return new WaitForSeconds(1.0f);

        transform.DOMoveZ(-10.0f, 6.0f);

        yield return new WaitForSeconds(9.0f);

        isFollowing = true;
    }
}
