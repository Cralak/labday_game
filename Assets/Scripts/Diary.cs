using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Diary : MonoBehaviour
{
    [SerializeField] TMP_Text text1;
    [SerializeField] TMP_Text text2;
    [SerializeField] AudioClip writingSound;
    [SerializeField] AudioClip pageSound;

    public List<string> events = new List<string>();

    Canvas canvas;
    AudioSource sound;
    bool isWriting;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        sound.volume = PlayerPrefs.GetFloat("SFX");
        if (canvas.enabled == true && !isWriting)
        {
            if (events.Contains("keyUsed"))
            {
                isWriting = true;
                StartCoroutine(Write("This door is so noisy... And that key is so rusty, glad I don't have to touch it anymore."));
                events.Remove("keyUsed");
            }
            else if (events.Contains("lightCorridor"))
            {
                isWriting = true;
                StartCoroutine(Write("What is illuminating the ceiling ? So scary! "));
                events.Remove("lightCorridor");
            }
        }
    }

    IEnumerator Write(string sentence)
    {
        sound.clip = writingSound;
        sound.Play();

        if (text1.text == "")
        {
            foreach (char letter in sentence)
            {
                text1.text += letter;
                yield return new WaitForSeconds(0.01f);
            }

            isWriting = false;
            sound.Stop();
        }
        else if (text2.text == "")
        {
            foreach (char letter in sentence)
            {
                text2.text += letter;
                yield return new WaitForSeconds(0.01f);
            }

            isWriting = false;
            sound.Stop();
        }
        else
        {
            text1.text = "";
            text2.text = "";

            sound.clip = pageSound;
            sound.Play();

            yield return new WaitForSeconds(0.5f);

            sound.Stop();
            StartCoroutine(Write(sentence));
        }
    }
}
