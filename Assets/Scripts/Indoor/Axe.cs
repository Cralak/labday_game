using UnityEngine;

public class Axe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("OpenedDiary").GetComponent<Diary>().CheckEvent("Axe")) Destroy(gameObject);
    }
}
