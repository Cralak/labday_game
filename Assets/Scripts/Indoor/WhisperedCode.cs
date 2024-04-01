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

    // Start is called before the first frame update
    void Awake()
    {
        if (KeyEvents.chessCode != null)
        {
            Destroy(transform.parent.gameObject);

            textField = GetComponentInChildren<TMP_Text>();

            isLaunched = false;
            code = UnityEngine.Random.Range(0, 99999).ToString();
            code = new string('0', 5 - code.Length) + code;
            KeyEvents.chessCode = code;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLaunched && KeyEvents.CheckEvent("chess"))
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
