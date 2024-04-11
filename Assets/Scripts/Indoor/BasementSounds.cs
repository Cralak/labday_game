using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementSounds : MonoBehaviour
{
    [SerializeField] AudioClip sound1;
    [SerializeField] AudioClip sound2;
    [SerializeField] AudioClip sound3;
    [SerializeField] AudioClip sound4;
    [SerializeField] AudioClip sound5;
    [SerializeField] AudioClip sound6;

    readonly List<AudioClip> sounds = new();
    GameObject player;
    Diary diary;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        source = GetComponent<AudioSource>();
        sounds.Add(sound1);
        sounds.Add(sound2);
        sounds.Add(sound3);
        sounds.Add(sound4);
        sounds.Add(sound5);
        sounds.Add(sound6);

        StartCoroutine(Noises());
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = PlayerPrefs.GetFloat("SFX") * (20.0f - Vector3.Distance(player.transform.position, transform.position)) / 20.0f;
    }

    IEnumerator Noises()
    {
        while (true)
        {
                source.clip = sounds[Random.Range(0, 6)];
                source.Play();

                yield return new WaitForSeconds(20.0f);
            
        }
    }
}
