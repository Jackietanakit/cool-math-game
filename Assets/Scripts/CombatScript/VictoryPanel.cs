using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryPanel : MonoBehaviour
{
    public TextMeshProUGUI victoryText;

    public void SetActive(bool active)
    {

        gameObject.SetActive(active);
    }
}
