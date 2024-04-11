using UnityEngine;

public class LoadCredits : MonoBehaviour
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
        if (isColliding && !UIState.isBusy && ToggleActions.IsPressed("interact")) StartCoroutine(Teleport.GoTo("Credits"));
    }

    void OnTriggerEnter()
    {
        isColliding = true;
    }

    void OnTriggerExit()
    {
        isColliding = false;
    }
}
