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
        StartCoroutine(LightCycle(interval));
    }

    // Update is called once per frame
    void Update()
    {
        lightSound.volume = PlayerPrefs.GetFloat("SFX");
    }

    IEnumerator LightCycle(float interval)
    {
        while (true)
        {
            for (int i = 0; i<200; i++)
            {
                // Increase light intensity and enable the light
                lightSound.UnPause();
                lightComponent.intensity = 1f;
                lightComponent.enabled = true;

                // Pause sound and disable the light
                yield return new WaitForSeconds(interval);
                lightSound.Pause();
                lightComponent.enabled = false;

                // Unpause sound, reset light intensity, and enable the light
                yield return new WaitForSeconds(interval);
                lightSound.UnPause();
                lightComponent.intensity = 0.5f;
                lightComponent.enabled = true;

                // Pause sound and disable the light
                yield return new WaitForSeconds(interval);
                lightSound.Pause();
                lightComponent.enabled = false;
            }
            yield return new WaitForSeconds(7);
        }
    }
}
