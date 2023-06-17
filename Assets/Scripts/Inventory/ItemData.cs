using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string id;
    public Sprite icon;
    public ItemType type;
    public EquipmentSlotType slotType;
    public List<Attribute> attributes;
}

public enum ItemType
{
    Consumable,
    Equipabble, 
}

public enum EquipmentSlotType
{
    // Define other equipment slots here
    None,
    Head,
    Body,
    Boots,
    MainWeapon,
    SecondaryWeapon
}

[System.Serializable]
public class Attribute
{
    public AttributeType type;
    public float value;

    public Attribute(AttributeType type, float value)
    {
        this.type = type;
        this.value = value;
    }
}

public enum AttributeType
{
    // Add other attribute types here
    HP,
    MP,
    STR,
    DEF,
    INT,
    AGI,
    VIT,
}