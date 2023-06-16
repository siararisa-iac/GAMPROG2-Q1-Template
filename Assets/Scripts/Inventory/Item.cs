using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public override void Interact()
    {
        // TODO: Add the item to the inventory. Make sure to destroy the prefab once the item is collected 
        // Add the item to the inventory
        InventoryManager.Instance.AddItem(id);
        // Destroy the item prefab
        Destroy(gameObject);
    }
}
