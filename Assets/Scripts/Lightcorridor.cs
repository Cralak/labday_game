using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightcorridor : MonoBehaviour
{
    [SerializeField] float interval = 0.3f; // Interval between light cycles

    GameObject player;
    Diary diary;
    Light lightComponent;
    AudioSource lightSound;
    bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        diary = GameObject.Find("Diary").GetComponent<Diary>();
        lightComponent = GetComponent<Light>();
        lightSound = GetComponent<AudioSource>();
        lightSound.Play();
        lightSound.Pause();
        hasStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        lightSound.volume = PlayerPrefs.GetFloat("SFX");

        // Check the distance between the light and the player to trigger the light cycle
        if (Vector3.Distance(transform.position, player.transform.position) < 10.0f && !hasStarted)
            StartCoroutine(LightCycle(interval));
    }

    IEnumerator LightCycle(float interval)
    {
        // Add an event to the diary when the light cycle starts
        diary.events.Add("lightCorridor");
        hasStarted = true;

        for (int i = 0; i < 10; i++)
        {
            // Increase light intensity and enable the light
            yield return new WaitForSeconds(interval);
            lightSound.UnPause();
            lightComponent.intensity = 3;
            lightComponent.enabled = true;

            // Pause sound and disable the light
            yield return new WaitForSeconds(interval);
            lightSound.Pause();
            lightComponent.enabled = false;

            // Unpause sound, reset light intensity, and enable the light
            yield return new WaitForSeconds(interval);
            lightSound.UnPause();
            lightComponent.intensity = 1;
            lightComponent.enabled = true;

            // Pause sound and disable the light
            yield return new WaitForSeconds(interval);
            lightSound.Pause();
            lightComponent.enabled = false;
        }
    }
}
