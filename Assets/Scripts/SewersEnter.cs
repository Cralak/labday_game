using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SewersEnter : MonoBehaviour
{
    [SerializeField] Canvas canvas; // Reference to the Canvas component

    Image blackScreen; // Reference to the Image component for the black screen effect
    GameObject player;
    PlayerMovement playerMovement;
    AudioSource footsteps;
    Diary diary;
    bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        footsteps = player.GetComponent<AudioSource>();
        diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        blackScreen = canvas.GetComponentInChildren<Image>();

        isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding && ToggleActions.IsPressed("interact")) Enter();
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
        diary.AddEvents("sewers");
        StartCoroutine(ChangeScene.GoTo(player, "sewers", new Vector3(0.0f, 1.0f, 0.0f)));
    }
}
