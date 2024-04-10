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
        print("beute");
        if(!diary.CheckEvent("ladyDisappeared") && diary.CheckEvent("FirstFloor")) diary.AddEvent("ladyDisappeared");
    }
}
