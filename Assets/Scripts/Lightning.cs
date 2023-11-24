using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    Light lightning;
    float LightningTime = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        lightning = GetComponent<Light>();
        StartCoroutine(Flash(30.0f));
    }

    // Update is called once per frame
    void Update()
    {
        return;
    }

    private IEnumerator Flash(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            lightning.intensity = 3f;
            lightning.enabled = true;
            yield return new WaitForSeconds(0.1f);
            lightning.enabled = false;
            for(int i = 0; i<3; i++)
            {
                yield return new WaitForSeconds(LightningTime);
                lightning.enabled = true;
                yield return new WaitForSeconds(LightningTime);
                lightning.enabled = false;
            }
            yield return new WaitForSeconds(LightningTime);
            lightning.enabled = true;
            yield return new WaitForSeconds(LightningTime);
            lightning.intensity = 2f;
            yield return new WaitForSeconds(LightningTime);
            lightning.intensity = 1f;
            yield return new WaitForSeconds(LightningTime);
            lightning.intensity = 0.5f;
            yield return new WaitForSeconds(LightningTime);
            lightning.enabled = false;
        }
    }
}
