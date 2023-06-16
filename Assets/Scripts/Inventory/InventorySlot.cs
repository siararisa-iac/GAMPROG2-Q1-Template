using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private ItemData itemData;
    public Image itemIcon;
    public Sprite defaultSprite;
    public Sprite swordSprite;
    public Sprite shieldSprite;
    public Sprite bowSprite;
    public Sprite hpPotionSprite;
    public Sprite mpPotionSprite;

    public void SetItem(ItemData data)
    {
        itemData = data;

        // Set the item icon based on the item type
        switch (itemData.type)
        {
            case ItemType.Equipabble:
                SetEquipabbleItemIcon();
                break;
            case ItemType.Consumable:
                SetConsumableItemIcon();
                break;
            default:
                // Set the default sprite for other item types
                itemIcon.sprite = defaultSprite;
                break;
        }

        // Enable the item icon to display it
        itemIcon.enabled = true;
    }

    private void SetEquipabbleItemIcon()
    {
        // Check the specific item IDs
        switch (itemData.id)
        {
            case "Sword":
                itemIcon.sprite = swordSprite;
                break;
            case "Shield":
                itemIcon.sprite = shieldSprite;
                break;
            case "Bow":
                itemIcon.sprite = bowSprite;
                break;
            default:
                // Set the default sprite for other equipabble item types
                itemIcon.sprite = defaultSprite;
                break;
        }
    }

    private void SetConsumableItemIcon()
    {
        // Check the specific item IDs
        switch (itemData.id)
        {
            case "HP Potion":
                itemIcon.sprite = hpPotionSprite;
                break;
            case "MP Potion":
                itemIcon.sprite = mpPotionSprite;
                break;
            default:
                // Set the default sprite for other consumable item types
                itemIcon.sprite = defaultSprite;
                break;
        }
    }


    public void UseItem()
    {
        if (itemData != null)
        {
            InventoryManager.Instance.UseItem(itemData);

            // Reset the item data and the icons
            itemData = null;
            itemIcon.sprite = null;
            itemIcon.enabled = false;
        }
        // TODO
        // Reset the item data and the icons here
    }

    public bool HasItem()
    {
        return itemData != null;
    }
}
