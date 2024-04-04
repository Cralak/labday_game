using UnityEngine;

public class SignClickMe : MonoBehaviour
{
    bool isTouching;
    Settings settings;

    // Start is called before the first frame update
    void Start()
    {
        isTouching = false;
        settings = GameObject.Find("Settings").GetComponent<Settings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTouching && !UIState.isBusy)
        {
            settings.ChangeSection("Controls Section");
            settings.ShowSettings();
        }

    }

    void OnTriggerEnter()
    {
        isTouching = true;
    }
    
    void OnTriggerExit()
    {
        isTouching = false;
    }
}
