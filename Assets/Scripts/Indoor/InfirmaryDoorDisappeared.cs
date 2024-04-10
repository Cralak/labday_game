using UnityEngine;

public class InfirmaryDoorDisappeared : MonoBehaviour
{
    Diary diary;

    // Start is called before the first frame update
    void Start()
    {
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
    }
    
    void OnBecameVisible()
    {
        if(!diary.CheckEvent("infirmaryDoorDisappeared") && diary.CheckEvent("InfirmaryKey")) diary.AddEvent("infirmaryDoorDisappeared");
    }
}
