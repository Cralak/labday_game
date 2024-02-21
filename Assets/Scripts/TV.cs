using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    [SerializeField] AudioClip TVSound;
    [SerializeField] AudioClip TVGlitch;

    AudioSource source;
    GameObject player;
    Diary diary;
    bool hasGlitched;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        source = GetComponent<AudioSource>();
        hasGlitched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 5.0f)
        {
            source.clip = TVGlitch;
            if (!source.isPlaying) source.Play();

            if (!hasGlitched)
            {
                hasGlitched = true;
                diary.events.Add("TV");
            }
        }
        else
        {
            source.clip = TVSound;
            if (!source.isPlaying) source.Play();
        }

        source.volume = PlayerPrefs.GetFloat("SFX") * (10.0f - Vector3.Distance(player.transform.position, transform.position)) / 10.0f;
    }
}
