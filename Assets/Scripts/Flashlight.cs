using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerCamera;
    [SerializeField] Light lightComponent;

    Inventory inventoryScript;

    // Start is called before the first frame update
    void Start()
    {
        inventoryScript = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryScript.inventory.Contains(gameObject))
        {
            transform.parent = playerCamera.transform;
            transform.localScale = new Vector3(1, 1, 1);
            transform.localPosition = new Vector3(0.4f, -0.3f, 0.2f);
            if (Input.GetKeyDown("f"))
            {
                lightComponent.enabled = !lightComponent.enabled;
            }
        }
    }
}
