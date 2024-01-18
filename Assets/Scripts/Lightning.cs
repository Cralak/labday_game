using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] float interval = 5.0f;
    [SerializeField] float lightningTime = 0.05f;

    Diary diary;
    AudioSource lightningSound;
    Light lightningLight;

    // Start is called before the first frame update
    void Start()
    {
        diary = GameObject.Find("Diary").GetComponent<Diary>();
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
        bool firstTime = true;
        while (true)
        {
            yield return new WaitForSeconds(interval);

            lightningLight.intensity = 3.0f;
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

            lightningLight.intensity = 2.0f;

            yield return new WaitForSeconds(lightningTime);

            lightningLight.intensity = 1.0f;

            yield return new WaitForSeconds(lightningTime);

            lightningLight.intensity = 0.5f;

            yield return new WaitForSeconds(lightningTime);

            lightningLight.enabled = false;

            if (firstTime)
            {
                diary.events.Add("lightning");
                firstTime = false;
            }
        }
    }
}
