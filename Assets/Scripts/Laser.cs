using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    Light laser;

    void Start()
    {
        laser = GetComponent<Light>();
    }

    void Update()
    {
        if (Input.GetKeyDown("l"))laser.enabled = !laser.enabled;

        if (laser.enabled)
        {
            if (Time.frameCount % 5 == 0)
            {
                laser.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f);
            }
        }
    }
}