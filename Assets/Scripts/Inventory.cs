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
        // Change inventory state if inventory key has been toggled
        ChangeInventoryState();

        // Display or hide the inventory based on its current state
        if (inventoryCanvas.enabled)
            DisplayInventory();
    }

    void DisplayInventory()
    {
        // Display inventory items in the slots
        for (int i = 0; i < inventory.Count; i++) inventorySlotsContent[i].texture = object2D[inventory[i]];

        // Fill remaining slots with a cross texture
        for (int i = inventory.Count; i < 6; i++) inventorySlotsContent[i].texture = cross;
    }

    void ToggleInventory()
    {
        // Switch UI and inventory's states
        inventoryCanvas.enabled = !inventoryCanvas.enabled;
        UI.enabled = !UI.enabled;
    }

    void ChangeInventoryState()
    {
        // Toggle the inventory on/off with the inventory key
        switch (PlayerPrefs.GetString("inventory"))
        {
            case "escape":
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ToggleInventory();
                }
                break;
            case "tab":
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    ToggleInventory();
                }
                break;
            case "lock":
                if (Input.GetKeyDown(KeyCode.CapsLock))
                {
                    ToggleInventory();
                }
                break;
            case "backspace":
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    ToggleInventory();
                }
                break;
            case "return":
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    ToggleInventory();
                }
                break;
            case "space":
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ToggleInventory();
                }
                break;
            case "shift":
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    ToggleInventory();
                }
                break;
            case "alt":
                if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
                {
                    ToggleInventory();
                }
                break;
            case "control":
                if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                {
                    ToggleInventory();
                }
                break;
            case "meta":
                if (Input.GetKeyDown(KeyCode.LeftMeta) || Input.GetKeyDown(KeyCode.RightMeta))
                {
                    ToggleInventory();
                }
                break;
            case "upArrow":
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ToggleInventory();
                }
                break;
            case "downArrow":
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ToggleInventory();
                }
                break;
            case "leftArrow":
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    ToggleInventory();
                }
                break;
            case "rightArrow":
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    ToggleInventory();
                }
                break;
            case "leftClick":
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    ToggleInventory();
                }
                break;
            case "rightClick":
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    ToggleInventory();
                }
                break;
            case "wheelClick":
                if (Input.GetKeyDown(KeyCode.Mouse2))
                {
                    ToggleInventory();
                }
                break;
            default:
                if (Input.GetKeyDown(PlayerPrefs.GetString("inventory")))
                {
                    ToggleInventory();
                }
                break;
        }
    }
}
