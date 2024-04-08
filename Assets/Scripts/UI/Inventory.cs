using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    readonly List<GameObject> inventory = new(); // List to store inventory items

    // Textures for inventory items
    [SerializeField] Texture2D cross;

    // List to store references to inventory slot content
    readonly List<RawImage> inventorySlotsContent = new();

    // Dictionary to associate 3D objects with their 2D representations
    readonly Dictionary<GameObject, Texture2D> object2D = new();

    GameObject keyObject; // Reference to the key GameObject
    Canvas UI; // Reference to the main UI canvas
    Canvas inventoryCanvas; // Reference to the inventory Canvas

    void Start()
    {
        // Get references to the UI Canvases
        UI = GameObject.Find("UI").GetComponent<Canvas>();
        inventoryCanvas = GetComponent<Canvas>();

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
        if (ToggleActions.IsPressed("inventory"))
        {
            if (!inventoryCanvas.enabled)
            {
                if (!UIState.isBusy) ShowInventory();
            }
            else
            {
                if (!UIState.isBusy) HideInventory();
            }
        }

        // Display or hide the inventory based on its current state
        if (inventoryCanvas.enabled) DisplayInventory();
    }

    void DisplayInventory()
    {
        // Display inventory items in the slots
        for (int i = 0; i < inventory.Count; i++) inventorySlotsContent[i].texture = object2D[inventory[i]];

        // Fill remaining slots with a cross texture
        for (int i = inventory.Count; i < 6; i++) inventorySlotsContent[i].texture = cross;
    }

    void HideInventory()
    {
        UI.enabled = true;
        inventoryCanvas.enabled = false;
    }

    void ShowInventory()
    {
        UI.enabled = false;
        inventoryCanvas.enabled = true;
    }

    public bool CheckInventory(GameObject thing)
    {
        return inventory.Contains(thing);
    }

    public void AddInventory(GameObject thing)
    {
        inventory.Add(thing);
    }

    public void RemoveInventory(GameObject thing)
    {
        inventory.Remove(thing);
    }

    public void SetTexture2D(GameObject object3D, Texture2D texture2D)
    {
        object2D[object3D] = texture2D;
    }
}
