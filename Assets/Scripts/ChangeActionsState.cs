using Unity.VisualScripting;
using UnityEngine;

public class ChangeActionsState : MonoBehaviour
{
    static public void DisableUI()
    {
        GameObject.Find("Inventory").GetComponent<Inventory>().enabled = false;
        GameObject.Find("OpenedDiary").GetComponent<Diary>().enabled = false;
        GameObject.Find("UI").GetComponent<Canvas>().enabled = false;
    }

    static public void EnableUI()
    {
        GameObject.Find("Inventory").GetComponent<Inventory>().enabled = true;
        GameObject.Find("OpenedDiary").GetComponent<Diary>().enabled = true;
        GameObject.Find("UI").GetComponent<Canvas>().enabled = true;
    }

    static public void DisablePlayer()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<AudioSource>().enabled = false;
    }

    static public void EnablePlayer()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<AudioSource>().enabled = true;
    }

    static public void HideFlashlight()
    {
        GameObject.Find("Flashlight").SetActive(false);
    }

    static public void ShowFlashlight()
    {
        GameObject.Find("Flashlight").SetActive(true);
    }

    static public void DisableAll()
    {
        DisableUI();
        DisablePlayer();
    }

    static public void EnableAll()
    {
        EnableUI();
        EnablePlayer();
    }
}
