using UnityEngine;

public class SewersEnter : MonoBehaviour
{
    GameObject player;
    bool isColliding;

    [SerializeField] AudioClip BGMClip;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding && !UIState.isBusy && ToggleActions.IsPressed("interact")) Enter();
    }

    void OnTriggerEnter()
    {
        isColliding = true;
    }

    void OnTriggerExit()
    {
        isColliding = false;
    }

    // Coroutine to load the hospital scene
    void Enter()
    {
        Destroy(GameObject.Find("BGM"));
        StartCoroutine(Teleport.GoTo(player, new Vector3(0.0f, 0f, 0.0f), "Sewers"));
        player.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        CreateSewersBGM();
    }

    void CreateSewersBGM()
    {
        GameObject music = new("BGM");
        AudioSource BGMSource = music.AddComponent<AudioSource>();
        BGMSource.clip = BGMClip;
        BGMSource.loop = true;
        BGMSource.Play();
        music.AddComponent<SetSFXVolume>();
        DontDestroyOnLoad(music);
    }
}
