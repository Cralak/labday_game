using UnityEngine;

public class ChangeActionsState : MonoBehaviour
{
    static bool cursorState;
    static bool UIState;
    static bool playerMovementState;
    static bool inventoryState;
    static bool diaryState;

    static public void DisableUI()
    {
        GameObject.Find("Inventory").GetComponent<Inventory>().enabled = false;
        GameObject.Find("OpenedDiary").GetComponent<Diary>().enabled = false;
        GameObject.Find("UI").GetComponent<Canvas>().enabled = false;
        GameObject.Find("Settings").GetComponent<Settings>().enabled = false;
    }

    static public void EnableUI()
    {
        GameObject.Find("Inventory").GetComponent<Inventory>().enabled = true;
        GameObject.Find("OpenedDiary").GetComponent<Diary>().enabled = true;
        GameObject.Find("UI").GetComponent<Canvas>().enabled = true;
        GameObject.Find("Settings").GetComponent<Settings>().enabled = true;
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

    static public void DisableAllAndSaveStates()
    {
        GameObject player = GameObject.Find("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        Diary diary = GameObject.Find("OpenedDiary").GetComponent<Diary>();
        Canvas UI = GameObject.Find("UI").GetComponent<Canvas>();


        cursorState = Cursor.lockState == CursorLockMode.Locked;
        playerMovementState = playerMovement.enabled;
        inventoryState = inventory.enabled;
        diaryState = diary.enabled;
        UIState = UI.enabled;

        Cursor.lockState = CursorLockMode.None;
        playerMovement.enabled = false;
        inventory.enabled = false;
        diary.enabled = false;
        UI.enabled = false;
        player.GetComponent<AudioSource>().enabled = false;
    }

    static public void RestoreAll()
    {
        Cursor.lockState = cursorState ? CursorLockMode.Locked : CursorLockMode.None;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = playerMovementState;
        GameObject.Find("Inventory").GetComponent<Inventory>().enabled = inventoryState;
        GameObject.Find("OpenedDiary").GetComponent<Diary>().enabled = diaryState;
        GameObject.Find("UI").GetComponent<Canvas>().enabled = UIState;
    }
}
