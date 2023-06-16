using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] protected Image itemIcon;

    protected ItemData itemData;
    
    public virtual void SetItem(ItemData data)
    {
        itemData = data;

        // Set the item icon
        itemIcon.sprite = itemData.icon;
        itemIcon.enabled = true; // Enable the item icon to display it
    }

    public virtual void RemoveItem()
    {
        itemIcon.sprite = null;
        itemData = null;
    }

    public bool HasItem()
    {
        return itemData != null;
    }
}
