using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Diary : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public List<string> events = new List<string>();

    Canvas canvas;
    AudioSource writingSound;
    bool isWriting;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        writingSound = GetComponent<AudioSource>();
        writingSound.Play();
        writingSound.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        writingSound.volume = PlayerPrefs.GetFloat("SFX");
        if (canvas.enabled == true && !isWriting)
        {
            if (events.Contains("keyUsed"))
            {
                StartCoroutine(Write("This door is so noisy... And that key is so rusty, glad I don't have to touch it anymore."));
                events.Remove("keyUsed");
            }
        }
    }

    IEnumerator Write(string sentence)
    {
        isWriting = true;
        writingSound.UnPause();

        string content = "";
        foreach (char letter in sentence)
        {
            content += letter;
            text.SetText(content);
            yield return new WaitForSeconds(0.01f);
        }

        isWriting = false;
        writingSound.Pause();
    }
}
