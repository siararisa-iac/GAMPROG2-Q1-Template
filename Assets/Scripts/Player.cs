using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHP;
    public float maxMP;
    public List<Attribute> attributes;


    public void RemoveAttributes(List<Attribute> removedAttributes)
    {
        foreach(Attribute att in removedAttributes)
        {
            for(int i = 0; i < attributes.Count; i++)
            {
                if(attributes[i].type == att.type)
                {
                    attributes[i].value -= att.value;
                }
            }
        }
    }

    public void AddAttributes(List<Attribute> addedAttributes)
    {
        foreach (Attribute att in addedAttributes)
        {
            for (int i = 0; i < attributes.Count; i++)
            {
                if (attributes[i].type == att.type)
                {
                    attributes[i].value += att.value;
                }
            }
        }
    }

    public float GetAttributeValue(AttributeType type)
    {
        foreach (Attribute att in attributes)
        {
            if(att.type == type)
            {
                return att.value;
            }
        }
        return 0;
    }
}
