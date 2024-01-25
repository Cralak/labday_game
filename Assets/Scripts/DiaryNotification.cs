using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiaryNotification : MonoBehaviour
{
    [SerializeField] Diary diary; // Reference to the Diary script

    TMP_Text number; // Reference to the TMP_Text component for displaying the number
    Image dot; // Reference to the Image component for the notification dot

    // Start is called before the first frame update
    void Start()
    {
        // Get references to the Image and TMP_Text components
        dot = GetComponentInChildren<Image>();
        number = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there are no events in the diary
        if (diary.events.Count == 0)
        {
            // Disable the notification dot and clear the number text
            dot.enabled = false;
            number.text = "";
        }
        else
        {
            // Enable the notification dot and display the number of events
            dot.enabled = true;
            number.text = diary.events.Count.ToString();
        }
    }
}
