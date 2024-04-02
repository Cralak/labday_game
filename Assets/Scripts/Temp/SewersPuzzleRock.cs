using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewersPuzzleRock : MonoBehaviour
{
    GameObject[] rocks = new GameObject[64]; 
    int n = 0;

    bool isTouching = false;
    
    void Start()
    {
        for (int i = 1; i <= 64; i++)
        {
            string rockName = "Rock" + i;
            GameObject rock = GameObject.Find(rockName);
            
            if (rock != null)
            {
                rocks[i - 1] = rock; 
            }
        }

    }
    void Update()
    {
        if (n < 64)
        {
            if (rocks[n].activeSelf && ToggleActions.IsPressed("interact") && isTouching)
            {
                rocks[n].SetActive(false);
                n = n +1;
            }
        }
    }

    void OnTriggerEnter()
    {
        isTouching = true;
    }

    void OnTriggerExit()
    {
        isTouching = false;
    }
}
