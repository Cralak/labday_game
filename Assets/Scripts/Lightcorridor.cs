using System.Collections;
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
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
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

        StartCoroutine(LightCycle(interval));
    }

    IEnumerator LightCycle(float interval)
    {
        // Add an event to the diary when the light cycle starts
        hasStarted = true;

        while (true)
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
