using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>();

    [SerializeField] Texture2D cross;
    [SerializeField] Texture2D cube;
    [SerializeField] Texture2D key2D;

    List<RawImage> inventorySlotsContent = new List<RawImage>();
    Dictionary<GameObject, Texture2D> object2D = new Dictionary<GameObject, Texture2D>();
    GameObject keyObject;

    bool isOpened = false;
    Canvas UI;
    Canvas inventoryCanvas;

    void Start()
    {
        UI = GameObject.Find("UI").GetComponent<Canvas>();
        inventoryCanvas = GameObject.Find("Inventory").GetComponent<Canvas>();

        if (keyObject = GameObject.Find("Key")) object2D[keyObject] = key2D;

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
            UI.enabled = !UI.enabled;
        }

        if (isOpened) displayInventory();
        inventoryCanvas.enabled = isOpened;
    }

    void displayInventory()
    {
        for (int i = 0; i < inventory.Count; i++) inventorySlotsContent[i].texture = object2D[inventory[i]];
        for (int i = inventory.Count; i < 6; i++) inventorySlotsContent[i].texture = cross;
    }
}
