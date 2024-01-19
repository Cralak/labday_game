using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] float interval = 5.0f;
    [SerializeField] float lightningTime = 0.05f;
    [SerializeField] AudioClip lightningSound;

    readonly List<AudioSource> lightnings = new();

    Diary diary;
    Light lightningLight;

    // Start is called before the first frame update
    void Start()
    {
        diary = GameObject.Find("Diary").GetComponent<Diary>();
        lightningLight = GetComponent<Light>();

        for (byte i = 0; i < 5; i++)
        {
            AudioSource lightning = gameObject.AddComponent<AudioSource>();
            lightning.clip = lightningSound;
            lightnings.Add(lightning);
        }

        StartCoroutine(Flash(interval));
    }

    // Update is called once per frame
    void Update()
    {
        for (byte i = 0; i < 5; i++)
        {
            lightnings[i].volume = PlayerPrefs.GetFloat("SFX");
        }
    }

    private IEnumerator Flash(float interval)
    {
        bool firstTime = true;
        while (true)
        {
            yield return new WaitForSeconds(interval);

            lightningLight.intensity = 3.0f;
            lightningLight.enabled = true;
            lightnings[3].Play();

            yield return new WaitForSeconds(0.1f);

            lightningLight.enabled = false;
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(lightningTime);

                lightningLight.enabled = true;
                lightnings[i].Play();

                yield return new WaitForSeconds(lightningTime);

                lightningLight.enabled = false;
            }
            yield return new WaitForSeconds(lightningTime);

            lightningLight.enabled = true;
            lightnings[4].Play();

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
