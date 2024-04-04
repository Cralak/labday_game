using UnityEngine;

public class SewersPuzzleRock : MonoBehaviour
{
    GameObject[] LBricks = new GameObject[36]; 
    int n = 0;

    bool isTouching = false;

    GameObject Bricks;
    
    void Start()
    {
        Bricks = GameObject.Find("Bricks");

        for (int i = 1; i <= 36; i++)
        {
            string brickName = "Brick" + i;
            GameObject brick = GameObject.Find(brickName);
            
            if (brick != null)
            {
                LBricks[i - 1] = brick; 
            }
        }

    }
    void Update()
    {
        if (n < 36)
        {
            if (LBricks[n].activeSelf && ToggleActions.IsPressed("interact") && isTouching)
            {
                LBricks[n].SetActive(false);
                n++;
            }
        }
        else
        {
            Bricks.SetActive(false);
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
