using UnityEngine;

public class Bricks : MonoBehaviour
{
    readonly GameObject[] bricks = new GameObject[36];
    int n = 0;
    bool isTouching = false;


    void Start()
    {
        for (int i = 1; i <= 36; i++)
        {
            string brickName = "Brick" + i;
            GameObject brick = GameObject.Find(brickName);

            if (brick != null)
            {
                bricks[i - 1] = brick;
            }
        }

    }
    void Update()
    {
        if (n < 36)
        {
            if (ToggleActions.IsPressed("interact") && isTouching)
            {
                Destroy(bricks[n]);
                n++;
            }
        }
        else
        {
            Destroy(gameObject);
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