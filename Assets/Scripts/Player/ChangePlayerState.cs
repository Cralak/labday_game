using UnityEngine;

public class ChangePlayerState : MonoBehaviour
{
    static public void Disable()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<AudioSource>().enabled = false;
    }

    static public void Enable()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<AudioSource>().enabled = true;
    }
}
