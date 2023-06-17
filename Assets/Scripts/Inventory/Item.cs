using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public override void Interact()
    {
        // Add the item to the inventory.
        if(InventoryManager.Instance.AddItem(this.id))
        {
            // Make sure to destroy the prefab once the item is collected 
            Destroy(this.gameObject);
        }
      
    }
}
