using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Camera inventoryCamera;
    public Camera playerCamera;
    private bool isInventoryOpen = false;
    private List<GameObject> inventoryContents = new List<GameObject>();

    void Update(){
        if (Input.GetKeyDown(KeyCode.Tab)){
            if (!isInventoryOpen){
                playerCamera.enabled = false;
                inventoryCamera.enabled = true;
                isInventoryOpen = true;
            } else{
                playerCamera.enabled = true;
                inventoryCamera.enabled = false;
                isInventoryOpen = false;
            }
        }
    }
}
