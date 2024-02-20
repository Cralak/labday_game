using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class validWords : MonoBehaviour
{
    StreamReader reader = new StreamReader("");
    private static List<string> validWordsList;

    void Start(){
        validWordsList = new List<string>();
    }

}
