using System.Collections;
using System.Collections.Generic;
using PSXShaderKit;
using UnityEngine;

public class SetShadder : MonoBehaviour
{
    PSXPostProcessEffect shadder;

    // Start is called before the first frame update
    void Start()
    {
        shadder = GetComponentInChildren<PSXPostProcessEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        shadder.enabled = PlayerPrefs.GetInt("shadder") == 1;
    }
}
