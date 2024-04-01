using System.Collections;
using UnityEngine;

public class TV : MonoBehaviour
{
    [SerializeField] AudioClip TVSound;
    [SerializeField] AudioClip TVGlitch;
    [SerializeField] float interval;
    [SerializeField] Color lightBlue;
    [SerializeField] Color darkBlue;

    AudioSource source;
    GameObject player;
    Diary diary;
    Light screenLight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        source = GetComponent<AudioSource>();
        screenLight = GetComponentInChildren<Light>();

        StartCoroutine(LightGlitch(interval));
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 5.0f)
        {
            source.clip = TVGlitch;
            if (!source.isPlaying) source.Play();

            if (!diary.IsEventAdded("TV")) diary.AddEvent("TV");

        }
        else
        {
            source.clip = TVSound;
            if (!source.isPlaying) source.Play();
        }

        source.volume = PlayerPrefs.GetFloat("SFX") * (4.0f - Vector3.Distance(player.transform.position, transform.position)) / 4.0f;
    }

    IEnumerator LightGlitch(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.1f);

                screenLight.color = darkBlue;

                yield return new WaitForSeconds(0.1f);

                screenLight.color = lightBlue;
            }

            yield return new WaitForSeconds(1.0f);

            screenLight.color = darkBlue;
        }
    }
}
