using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInfirmary : MonoBehaviour
{
    bool isTouching = false;
    int nb = 0;
    [SerializeField] GameObject ghost;

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
        print("ghost ON");
        yield return new WaitForSeconds(1f);
        ghost.SetActive(false);
        print("ghost OFF");
        nb = nb + 1;
    }

    void OnTriggerEnter()
    {
        print("Touch");
        isTouching = true;
    }
}
