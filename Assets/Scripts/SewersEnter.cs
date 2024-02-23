using UnityEngine;

public class SewersEnter : MonoBehaviour
{
    GameObject player;
    Diary diary;
    bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();

        isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding && !UIState.isBusy && ToggleActions.IsPressed("interact")) Enter();
    }

    void OnTriggerEnter(Collider collider)
    {
        isColliding = true;
    }

    void OnTriggerExit(Collider collider)
    {
        isColliding = false;
    }

    // Coroutine to load the hospital scene
    void Enter()
    {
        diary.AddEvent("sewers");
        Destroy(GameObject.Find("BGM"));
        StartCoroutine(Teleport.GoTo(player, new Vector3(0.0f, 1.0f, 0.0f), "Sewers"));
    }
}
