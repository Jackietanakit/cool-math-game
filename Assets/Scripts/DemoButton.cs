using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoButton : MonoBehaviour
{
    public TextMeshProUGUI consoleText;

    public void SetDifficultyTo(float difficulty)
    {
        GameManager.instance._playerInventory.SetDifficultyTo(difficulty);
        SetConsole("Difficulty is set to " + difficulty);
    }

    public void SetMoneyTo(int amount)
    {
        GameManager.instance._playerInventory.SetMoneyTo(amount);
        SetConsole("Money is set to " + amount);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadCombatTutorial()
    {
        TutorialManager.Instance.LoadCombatTutorialScene();
    }

    public void LoadBoss()
    {
        GameManager.instance.IsInElite = false;
        GameManager.instance.IsInBoss = true;
        ScenesManager.Instance.LoadCombatScene();
    }

    public void LoadCombat()
    {
        GameManager.instance.IsInElite = false;
        GameManager.instance.IsInBoss = false;
        ScenesManager.Instance.LoadCombatScene();
    }

    public void SetConsole(string name)
    {
        consoleText.text = name;
    }

    public void KillFirstEnemy()
    {
        CombatManager.Instance.GetNearestEnemies(1)[0].Die();
    }
}
