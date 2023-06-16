using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class DisplayManaBar : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image fill;

    private void UpdateBar()
    {
        Player player = InventoryManager.Instance.player;
        float currentHealth = player.GetAttributeValue(AttributeType.MP);
        fill.fillAmount = currentHealth / player.maxMP;
        text.text = ($"MP {currentHealth} / {player.maxMP}");
    }

    private void Update()
    {
        UpdateBar();
    }
}