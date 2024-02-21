using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCredits : MonoBehaviour
{
    void OnTriggerEnter(Collider Collider)
    {
        GameObject sacrifice = new GameObject("sacrifice");
        DontDestroyOnLoad(sacrifice);

        foreach (GameObject thing in sacrifice.scene.GetRootGameObjects()) Destroy(thing);

        SceneManager.LoadScene("Credits");
    }
}
