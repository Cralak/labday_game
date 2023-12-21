using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightcorridor : MonoBehaviour
{
    bool enabled = true;
    Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 10 == 0 && enabled)
        {
            if (light.intensity == 1 && !light.enabled)
            {
                light. intensity = 3;
            } 
            else if (light.intensity == 3 && !light.enabled)
            {
                light.intensity = 1;
            }            
            
            light.enabled = !light.enabled;
        }
        if (Time.frameCount % 600 == 0)
        {
            light.enabled = false;
            enabled = !enabled;
        }
    }
}
