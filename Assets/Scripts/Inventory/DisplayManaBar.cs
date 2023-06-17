using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManaBar : AttributeBar
{
    protected override void Start()
    {
        base.Start();
        maxValue = player.maxMP;
    }
}
