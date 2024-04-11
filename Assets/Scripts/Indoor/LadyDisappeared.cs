using UnityEngine;

public class LadyDisappeared : MonoBehaviour
{
    Diary diary;

    // Start is called before the first frame update
    void Start()
    {
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
    }
    
    void OnTriggerEnter()
    {
        if(!diary.CheckEvent("ladyDisappeared") && diary.CheckEvent("firstFloor")) diary.AddEvent("ladyDisappeared");
    }
}
