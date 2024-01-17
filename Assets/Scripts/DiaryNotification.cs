using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiaryNotification : MonoBehaviour
{
    [SerializeField] Diary diary;
    [SerializeField] TMP_Text number;

    Image dot;

    // Start is called before the first frame update
    void Start()
    {
        dot = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (diary.events.Count == 0)
        {
            dot.enabled = false;
            number.SetText("");
        }
        else
        {
            dot.enabled = true;
            number.SetText(diary.events.Count.ToString());
        }
    }
}
