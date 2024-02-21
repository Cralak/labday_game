using UnityEngine;
using UnityEngine.SceneManagement;

public class ClimbLadder : MonoBehaviour
{
    bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding && ToggleActions.IsPressed("interact"))
        {
            SceneManager.LoadScene("OutdoorScene");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        isColliding = true;
    }

    void OnTriggerExit(Collider collider)
    {
        isColliding = false;
    }
}
