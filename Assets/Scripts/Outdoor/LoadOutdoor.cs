using UnityEngine;

public class LoadOutdoor : MonoBehaviour
{
    public void LoadScene()
    {
        // Start the asynchronous loading process
        StartCoroutine(Teleport.GoTo("OutdoorScene"));
    }
}
