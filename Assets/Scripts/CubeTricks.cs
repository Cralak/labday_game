using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTricks : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitAndPrint(2.0f, 0.1f));
    }

    private IEnumerator WaitAndPrint(float longTime, float shortTime)
    {
        while (true)
        {
            for (short i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(shortTime);
                transform.position += new Vector3(-0.1f, 0f, 0f);
            }
            yield return new WaitForSeconds(longTime);
            transform.position += new Vector3(-1f, 0f, 0f);
            yield return new WaitForSeconds(longTime);
        }
    }

}
