using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>();
    [SerializeField] Texture2D cross;
    [SerializeField] Texture2D cube;
    [SerializeField] Texture2D flashlight;
    [SerializeField] GameObject inventoryCanvas;

    List<RawImage> inventorySlotsContent = new List<RawImage>();
    Dictionary<GameObject, Texture2D> object2D = new Dictionary<GameObject, Texture2D>();
    bool isOpened = false;
    Canvas inventoryScreen;

    void Start()
    {
        // object2D[GameObject.Find("Cube (1)")] = cube;
        object2D[GameObject.Find("Flashlight")] = flashlight;
        // object2D[GameObject.Find("Cube (2)")] = cube;

        inventoryScreen = inventoryCanvas.GetComponent<Canvas>();
        for (int i = 0; i < 6; i++)
        {
            inventorySlotsContent.Add(GameObject.Find("InventorySlot" + i + "Content").GetComponent<RawImage>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpened = !isOpened;
        }

        if (isOpened)
        {
            displayInventory();
        }

        inventoryScreen.enabled = isOpened;
    }

    void displayInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            inventorySlotsContent[i].texture = object2D[inventory[i]];
        }

        for (int i = inventory.Count; i < 6; i++)
        {
            inventorySlotsContent[i].texture = cross;
        }


    }
}
