using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class WhisperedCode : MonoBehaviour
{
    [SerializeField, Range(0.1f, 1.0f)] float delay = 0.4f;

    TMP_Text textField;
    string code;
    bool isLaunched;
    Diary diary;

    void Awake()
    {
        if (KeyEvents.chessCode != null)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        textField = GetComponentInChildren<TMP_Text>();
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();

        isLaunched = false;
        code = UnityEngine.Random.Range(0, 99999).ToString();
        code = new string('0', 5 - code.Length) + code;
        KeyEvents.chessCode = code;
        diary.SetEventText("chess", "Why did I have to play chess in this place with that scary old mother? And what is it that she whispered? " + code + ", I wonder what it could be");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLaunched && diary.CheckEvent("chess"))
        {
            StartCoroutine(Write());
            isLaunched = true;
        }
    }

    IEnumerator Write()
    {
        for (int j = 0; j < 3; j++)
        {
            // Display text letter by letter with a delay
            for (int i = 0; i < code.Length; i++)
            {
                // Check if chessScript is playing and exit the coroutine
                textField.text += code[i];

                yield return new WaitForSeconds(delay);
            }

            textField.text += '.';

            yield return new WaitForSeconds(delay);

            textField.text += '.';

            yield return new WaitForSeconds(delay);

            textField.text += '.';

            yield return new WaitForSeconds(delay);

            textField.text += ' ';

            yield return new WaitForSeconds(delay);
        }

        Destroy(gameObject);
    }
}
