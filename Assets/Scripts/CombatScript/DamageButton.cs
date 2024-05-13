using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageButton : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI damagetext;

    public void DealDamage()
    {
        CombatManager.Instance.ChangeGameState(GameState.AfterPlayerTurn);
    }

    public void updateText(string text)
    {
        if (!this.isActiveAndEnabled)
        {
            this.gameObject.SetActive(true);
        }
        damagetext.text = text;
    }

    public void setInactive()
    {
        this.gameObject.SetActive(false);
    }
}
