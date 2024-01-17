using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] float interval = 30f;
    [SerializeField] float lightningTime = 0.05f;
    AudioSource lightningSound;
    Light lightningLight;

    // Start is called before the first frame update
    void Start()
    {
        lightningLight = GetComponent<Light>();
        lightningSound = GetComponent<AudioSource>();
        StartCoroutine(Flash(interval));
    }

    // Update is called once per frame
    void Update()
    {
        lightningSound.volume = PlayerPrefs.GetFloat("SFX");
    }

    private IEnumerator Flash(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            lightningLight.intensity = 3f;
            lightningLight.enabled = true;
            lightningSound.Play();
            yield return new WaitForSeconds(0.1f);
            lightningLight.enabled = false;
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(lightningTime);
                lightningLight.enabled = true;
                lightningSound.Play();
                yield return new WaitForSeconds(lightningTime);
                lightningLight.enabled = false;
            }
            yield return new WaitForSeconds(lightningTime);
            lightningLight.enabled = true;
            lightningSound.Play();
            yield return new WaitForSeconds(lightningTime);
            lightningLight.intensity = 2f;
            yield return new WaitForSeconds(lightningTime);
            lightningLight.intensity = 1f;
            yield return new WaitForSeconds(lightningTime);
            lightningLight.intensity = 0.5f;
            yield return new WaitForSeconds(lightningTime);
            lightningLight.enabled = false;
        }
    }
}
