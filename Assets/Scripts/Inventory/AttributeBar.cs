using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttributeBar : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI text;
    [SerializeField] protected Image fill;
    [SerializeField] protected AttributeType attributeToDisplay;

    protected Player player;
    protected float maxValue;
    
    protected virtual void Start()
    {
        player = InventoryManager.Instance.player;
    }

    protected void UpdateBar()
    {
        float currentValue = player.GetAttributeValue(attributeToDisplay);
        fill.fillAmount = currentValue / maxValue;
        text.text = ($"{attributeToDisplay.ToString()} {currentValue} / {maxValue}");
    }

    protected void Update()
    {
        UpdateBar();
    }
}
