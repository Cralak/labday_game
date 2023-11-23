using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleLaser : MonoBehaviour
{
    Light laser;

    void Start()
    {
        laser = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            laser.enabled = !laser.enabled;
        }

        if (Time.frameCount % 5 == 0)
        {
            laser.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f);
        }
    }
}
