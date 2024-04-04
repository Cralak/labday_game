using TMPro;
using UnityEngine;

public class WordleUI : MonoBehaviour
{
    [SerializeField] GameObject rules;
    [SerializeField] GameObject grid;

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

    public void Exit()
    {
        StartCoroutine(wordle.Unplay());
    }

    public void Rules()
    {
        rules.SetActive(!rules.activeSelf);
        grid.SetActive(!grid.activeSelf);
        GetComponentInChildren<TMP_Text>().text = rules.activeSelf ? "Back" : "Rules";
    }
}
