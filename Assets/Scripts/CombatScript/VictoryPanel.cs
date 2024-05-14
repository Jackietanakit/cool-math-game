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
        SetActive(true);
        SetText(Text);
    }

    void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    void SetText(string Text)
    {
        victoryText.text = Text;
    }
}
