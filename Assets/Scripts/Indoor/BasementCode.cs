using UnityEngine;

public class BasementCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string code = Random.Range(0, 99999).ToString();
        code = new string('0', 5 - code.Length) + code;
        KeyEvents.basementCode = code;
        GetComponent<DisplayPaper>().SetText(code);
    }
}
