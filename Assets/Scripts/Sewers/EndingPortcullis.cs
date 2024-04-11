using UnityEngine;

public class EndingPortcullis : MonoBehaviour
{
    bool first = false;
    bool second = false;
    bool third = false;
    bool isTouching = false;

    // Update is called once per frame
    void Update()
    {
        if (isTouching)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                first = true;
                second = false;
                third = false;
            }
            else if (Input.GetKeyDown(KeyCode.I) && first)
            {
                first = false;
                second = true;
            }
            else if (Input.GetKeyDown(KeyCode.O) && second)
            {
                second = false;
                third = true;
            }
            else if (Input.GetKeyDown(KeyCode.U) && third)
            {
                transform.position += new Vector3(0f, 0.1f, 0f);
                third = false;
            }
            // Si une autre touche est enfoncée sans respecter l'ordre, réinitialise les variables
            else if (Input.anyKeyDown)
            {
                first = false;
                second = false;
                third = false;
            }
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