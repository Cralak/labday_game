using System.Collections;
using UnityEngine;

public class GhostInfirmary : MonoBehaviour
{
    [SerializeField] GameObject ghost;

    bool isTouching = false;
    int nb = 0;

    void Start()
    {
        ghost.SetActive(false);
    }

    void Update()
    {
        if (isTouching && KeyEvents.wordleCode != null && nb < 60)
        {
            StartCoroutine(Pop());
        }
    }

    IEnumerator Pop()
    {
        ghost.SetActive(true);

        yield return new WaitForSeconds(1f);

        ghost.SetActive(false);
        nb++;
    }

    void OnTriggerEnter()
    {
        isTouching = true;
    }
}
