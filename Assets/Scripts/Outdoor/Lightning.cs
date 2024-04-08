using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] float interval = 5.0f; // Interval between lightning flashes
    [SerializeField] float lightningTime = 0.05f; // Duration of the lightning flash
    [SerializeField] AudioClip lightningSound; // Sound clip for the lightning

    readonly List<AudioSource> lightnings = new(); // List to store AudioSource components for lightning sounds

    Diary diary;
    Light lightningLight;

    // Start is called before the first frame update
    void Start()
    {
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        lightningLight = GetComponent<Light>();

        // Create five AudioSource components for different lightning sounds
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
        // Adjust the volume of each lightning sound based on player preferences
        for (byte i = 0; i < 5; i++)
        {
            lightnings[i].volume = PlayerPrefs.GetFloat("SFX");
        }
    }

    private IEnumerator Flash(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // Start the lightning flash
            lightningLight.intensity = 3.0f;
            lightningLight.enabled = true;
            lightnings[3].Play();

            yield return new WaitForSeconds(0.1f);

            // End the lightning flash
            lightningLight.enabled = false;

            // Iterate through the remaining lightning flashes
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(lightningTime);

                // Start the next lightning flash
                lightningLight.enabled = true;
                lightnings[i].Play();

                yield return new WaitForSeconds(lightningTime);

                // End the next lightning flash
                lightningLight.enabled = false;
            }

            yield return new WaitForSeconds(lightningTime);

            // Start the final lightning flash
            lightningLight.enabled = true;
            lightnings[4].Play();

            yield return new WaitForSeconds(lightningTime);

            // Adjust the lightning intensity over the next few seconds
            lightningLight.intensity = 2.0f;

            yield return new WaitForSeconds(lightningTime);

            lightningLight.intensity = 1.0f;

            yield return new WaitForSeconds(lightningTime);

            lightningLight.intensity = 0.5f;

            yield return new WaitForSeconds(lightningTime);

            // End the lightning flash
            lightningLight.enabled = false;
        }
    }
}
