using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private Image defaultIcon;
    [SerializeField] private Image itemIcon;
    public EquipmentSlotType type;

    private ItemData itemData;

    public void SetItem(ItemData data)
    {
        // Set the item data the and icons here
        itemData = data;
        // Make sure to apply the attributes once an item is equipped
        InventoryManager.Instance.player.AddAttributes(itemData.attributes);
        defaultIcon.gameObject.SetActive(false);
        itemIcon.gameObject.SetActive(true);
        itemIcon.sprite = data.icon;
    }

    public void Unequip()
    {
        // Check if there is an available inventory slot before removing the item.
        InventorySlot emptySlot = InventoryManager.Instance.GetEmptyInventorySlot();
        if (emptySlot != null)
        {
            // Make sure to return the equipment to the inventory when there is an available slot.
            emptySlot.SetItem(itemData);
            InventoryManager.Instance.player.RemoveAttributes(itemData.attributes);
            // Reset the item data and icons here
            defaultIcon.gameObject.SetActive(true);
            itemIcon.gameObject.SetActive(false);
            itemIcon.sprite = null;
            itemData = null;
        }
    }

    public bool HasItem()
    {
        return itemData != null;
    }
}
