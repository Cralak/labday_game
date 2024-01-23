using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inventory = new(); // List to store inventory items

    // Textures for inventory items
    [SerializeField] Texture2D cross;
    [SerializeField] Texture2D cube;
    [SerializeField] Texture2D key2D;

    // List to store references to inventory slot content
    readonly List<RawImage> inventorySlotsContent = new();

    // Dictionary to associate 3D objects with their 2D representations
    readonly Dictionary<GameObject, Texture2D> object2D = new();

    GameObject keyObject; // Reference to the key GameObject
    bool isOpened = false; // Flag to check if the inventory is open
    Canvas UI; // Reference to the main UI Canvas
    Canvas inventoryCanvas; // Reference to the inventory Canvas

    void Start()
    {
        // Get references to the UI Canvases
        UI = GameObject.Find("UI").GetComponent<Canvas>();
        inventoryCanvas = GameObject.Find("Inventory").GetComponent<Canvas>();

        // Find and store the reference to the key GameObject
        if (keyObject = GameObject.Find("Key")) 
            object2D[keyObject] = key2D;

        // Get references to the inventory slot content RawImages
        for (int i = 0; i < 6; i++)
        {
            inventorySlotsContent.Add(GameObject.Find("InventorySlot" + i + "Content").GetComponent<RawImage>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle the inventory on/off with the Tab key
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpened = !isOpened;
            UI.enabled = !UI.enabled;
        }

        // Display or hide the inventory based on its current state
        if (isOpened) 
            DisplayInventory();
        inventoryCanvas.enabled = isOpened;
    }

    void DisplayInventory()
    {
        // Display inventory items in the slots
        for (int i = 0; i < inventory.Count; i++) 
            inventorySlotsContent[i].texture = object2D[inventory[i]];
        
        // Fill remaining slots with a cross texture
        for (int i = inventory.Count; i < 6; i++) 
            inventorySlotsContent[i].texture = cross;
    }
}
