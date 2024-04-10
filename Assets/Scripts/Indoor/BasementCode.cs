using UnityEngine;

public class BasementCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string code = Random.Range(0, 99999).ToString();
        code = new string('0', 5 - code.Length) + code;
        KeyEvents.basementCode = code;
        GetComponent<DisplayPaper>().SetText("Remaining medication stock: Paracetamol, Sedatives, Antipsychotics, Mood Stabilizers. If more medication is needed, the reserve is in one of the rooms in the basement. Access code to the basement door if needed: " + code + " . Warning! Only go there if necessary.");
    }
}