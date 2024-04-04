using UnityEngine;

public class WordleUI : MonoBehaviour
{
    Wordle wordle;
    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        wordle = transform.parent.GetComponent<Wordle>();
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        canvas.enabled = wordle.isPlaying;
    }
}
