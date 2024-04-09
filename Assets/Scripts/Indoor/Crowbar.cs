using UnityEngine;

public class Crowbar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("OpenedDiary").GetComponent<Diary>().CheckEvent("Crowbar")) Destroy(gameObject);
    }
}
