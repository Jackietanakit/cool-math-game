using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryPanel : MonoBehaviour
{
    public TextMeshProUGUI victoryText;

    public void ShowPanel(string Text)
    {
        gameObject.SetActive(true);
        SetText(Text);
    }

    void SetText(string Text)
    {
        victoryText.text = Text;
    }
}
