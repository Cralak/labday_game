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
        if (Input.GetKeyDown(KeyCode.L)) laser.enabled = !laser.enabled;
        if (laser.enabled && Time.frameCount % 5 == 0) laser.color = Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 0.0f, 1.0f);
    }
}