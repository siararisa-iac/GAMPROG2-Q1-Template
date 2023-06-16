using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Player player;
    //For now, this will store information of the Items that can be added to the inventory
    public List<ItemData> itemDatabase;

    //Store all the inventory slots in the scene here
    public List<InventorySlot> inventorySlots;

    //Store all the equipment slots in the scene here
    public List<EquipmentSlot> equipmentSlots;

    //Singleton implementation. Do not change anything within this region.
    #region SingletonImplementation
    private static InventoryManager instance = null;
    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "Inventory";
                    instance = go.AddComponent<InventoryManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    private void Start()
    {
        itemDatabase = new List<ItemData>();

        // Create Sword item
        ItemData sword = CreateItemData("Sword", "SwordIcon", ItemType.Equipabble, EquipmentSlotType.Equippable);
        sword.attributes = new List<Attribute>
    {
        new Attribute(AttributeType.Strength, 10),
        new Attribute(AttributeType.Dexterity, 5),
    };
        itemDatabase.Add(sword);

        // Create Shield item
        ItemData shield = CreateItemData("Shield", "ShieldIcon", ItemType.Equipabble, EquipmentSlotType.Equippable);
        shield.attributes = new List<Attribute>
    {
        new Attribute(AttributeType.Defense, 15),
    };
        itemDatabase.Add(shield);

        // Create Bow item
        ItemData bow = CreateItemData("Bow", "BowIcon", ItemType.Equipabble, EquipmentSlotType.Equippable);
        bow.attributes = new List<Attribute>
    {
        new Attribute(AttributeType.Dexterity, 10),
    };
        itemDatabase.Add(bow);

        // Create HP Potion item
        ItemData hpPotion = CreateItemData("HP Potion", "HPPotionIcon", ItemType.Consumable, EquipmentSlotType.Consumable);
        hpPotion.attributes = new List<Attribute>
    {
        new Attribute(AttributeType.HP, 50),
    };
        itemDatabase.Add(hpPotion);

        // Create MP Potion item
        ItemData mpPotion = CreateItemData("MP Potion", "MPPotionIcon", ItemType.Consumable, EquipmentSlotType.Consumable);
        mpPotion.attributes = new List<Attribute>
    {
        new Attribute(AttributeType.MP, 50),
    };
        itemDatabase.Add(mpPotion);

        // Create Key item
        ItemData key = CreateItemData("Key", "KeyIcon", ItemType.Consumable, EquipmentSlotType.None);
        itemDatabase.Add(key);
    }

    private ItemData CreateItemData(string id, string iconResourceName, ItemType type, EquipmentSlotType slotType)
    {
        ItemData itemData = new ItemData();
        itemData.id = id;
        itemData.icon = Resources.Load<Sprite>(iconResourceName);
        itemData.type = type;
        itemData.slotType = slotType;
        return itemData;
    }

    public void UseItem(ItemData data)
    {
        // TODO
        // If the item is a consumable, simply add the attributes of the item to the player.
        // If it is equippable, get the equipment slot that matches the item's slot.
        // Set the equipment slot's item as that of the used item
    }

   
    public void AddItem(string itemID)
    {
        //TODO
        //1. Cycle through every item in the database until you find the item with the same id.
        //2. Get the index of the InventorySlot that does not have any Item and set its Item to the Item found

        // Find the item in the item database
        ItemData itemToAdd = itemDatabase.Find(item => item.id == itemID);

        if (itemToAdd != null)
        {
            // Find an empty inventory slot
            InventorySlot emptySlot = inventorySlots.Find(slot => !slot.HasItem());

            if (emptySlot != null)
            {
                // Set the item of the empty slot to the item found
                emptySlot.SetItem(itemToAdd);
                Debug.Log("Added Item: " + itemID);
            }
        }
    }

    public int GetEmptyInventorySlot()
    {

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (!inventorySlots[i].HasItem())
            {
                return i;
            }
        }
        //TODO
        //Check which inventory slot doesn't have an Item and return its index
        return -1;
    }

    public int GetEquipmentSlot(EquipmentSlotType type)
    {
        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            if (equipmentSlots[i].type == type)
            {
                return i;
            }
        }
        //TODO
        //Check which equipment slot matches the slot type and return its index
        return -1;
    }
}
