using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightcorridor : MonoBehaviour
{
    [SerializeField] float interval = 0.3f;

    Light lightComponent;
    AudioSource lightSound;

    // Start is called before the first frame update
    void Start()
    {
        lightComponent = GetComponent<Light>();
        lightSound = GetComponent<AudioSource>();
        lightSound.Play();
        StartCoroutine(LightCycle(interval));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LightCycle(float interval)
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(interval);

            lightSound.UnPause();
            lightComponent.intensity = 3;
            lightComponent.enabled = true;

            yield return new WaitForSeconds(interval);

            lightSound.Pause();
            lightComponent.enabled = false;

            yield return new WaitForSeconds(interval);

            lightSound.UnPause();
            lightComponent.intensity = 1;
            lightComponent.enabled = true;

            yield return new WaitForSeconds(interval);

            lightSound.Pause();
            lightComponent.enabled = false;
        }
    }
}
