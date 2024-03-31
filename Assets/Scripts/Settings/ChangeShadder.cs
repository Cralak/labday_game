using UnityEngine;
using UnityEngine.UI;

public class ChangeShadder : MonoBehaviour
{
    RawImage square;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("shadder")) PlayerPrefs.SetInt("shadder", 1);
    }

    void Start()
    {
        square = GetComponentInChildren<RawImage>();
    }

    public void ToggleShadder()
    {
        PlayerPrefs.SetInt("shadder", (PlayerPrefs.GetInt("shadder") + 1) % 2);
        square.enabled = PlayerPrefs.GetInt("shadder") == 1;
    }
}
