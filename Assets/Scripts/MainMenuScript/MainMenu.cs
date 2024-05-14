using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI informationText;

    [SerializeField]
    TextMeshProUGUI titleText;
    public float HighestDifficulty = 0;

    [SerializeField]
    Button NormalButton;

    [SerializeField]
    Button HardButton;

    [SerializeField]
    Button ChallengingButton;

    [SerializeField]
    Button ExtremeButton;

    [SerializeField]
    TextMeshProUGUI NormalText;

    [SerializeField]
    TextMeshProUGUI HardText;

    [SerializeField]
    TextMeshProUGUI ChallengingText;

    [SerializeField]
    TextMeshProUGUI ExtremeText;

    [SerializeField]
    GameObject TutorialPanel;

    public void Start()
    {
        Test();
        if (PlayerPrefs.HasKey("HighestDifficulty"))
        {
            var highestDifficulty = PlayerPrefs.GetFloat("HighestDifficulty");
            HighestDifficulty = highestDifficulty;
        }
        SetDisableDifficulty();
    }

    public void GetDifficultyInfo(string option)
    {
        switch (option)
        {
            case "Easy":
                titleText.text = "Easy";
                informationText.text =
                    "Initial Difficulty: 1"
                    + "\nStarting Coin: 300"
                    + "\nReaching 1 Difficulty"
                    + "\nEasy Mode!";
                break;
            case "Normal":
                titleText.text = "Normal";
                informationText.text =
                    "Initial Difficulty: 1.35"
                    + "\nStarting Coin: 100"
                    + "\nReaching 2 Difficulty"
                    + "\nNormal Mode!";
                break;
            case "Hard":
                titleText.text = "Hard";
                informationText.text =
                    "Initial Difficulty: 1.5"
                    + "\nStarting Coin: 50"
                    + "\nReaching 2.5 Difficulty"
                    + "\nEnemy each combat: + 1"
                    + "\nHard Mode!";
                break;
            case "Challenging":
                titleText.text = "Challenging";
                informationText.text =
                    "Initial Difficulty: 1.8"
                    + "\nStarting Coin: 0"
                    + "\nReaching 3 Difficulty"
                    + "\nEnemy each combat: + 1"
                    + "\nChallenging Mode!";
                break;
            case "Extreme":
                titleText.text = "Extreme";
                informationText.text =
                    "Initial Difficulty: 2"
                    + "\nStarting Coin: 0"
                    + "\nReaching 3.5 Difficulty"
                    + "\nEnemy each combat: + 2"
                    + "\nExtreme Mode!";
                break;
            default:
                break;
        }
    }

    public void Test()
    {
        PlayerPrefs.SetFloat("HighestDifficulty", 2.1f);
        PlayerPrefs.DeleteKey("Tutorial");
    }

    public void SetDisableDifficulty()
    {
        if (HighestDifficulty < 2.0f)
        {
            NormalButton.interactable = false;
            NormalText.text = "Locked";
        }
        if (HighestDifficulty < 2.5f)
        {
            HardButton.interactable = false;
            HardText.text = "Locked";
        }
        if (HighestDifficulty < 3.0f)
        {
            ChallengingButton.interactable = false;
            ChallengingText.text = "Locked";
        }
        if (HighestDifficulty < 3.5f)
        {
            ExtremeButton.interactable = false;
            ExtremeText.text = "Locked";
        }
    }

    public void ContinueGame()
    {
        GameManager.instance.IsNewGame = false;
        GameManager.instance._playerInventory.LoadInventory();
        ScenesManager.Instance.LoadNewGame();
    }

    public void NewGame(string difficulty)
    {
        if (PlayerPrefs.HasKey("Tutorial"))
        {
            GameManager.instance.InitializePlayerInventory(difficulty);
            GameManager.instance.IsNewGame = true;
            PlayerPrefs.DeleteKey("Map");
            ScenesManager.Instance.LoadNewGame();
        }
        else
        {
            TutorialPanel.SetActive(true);
        }
    }

    public void Tutorial(string isTutorial)
    {
        PlayerPrefs.SetString("Tutorial", isTutorial);
        GameManager.instance.InitializePlayerInventory("easy");
        GameManager.instance.IsNewGame = true;
        PlayerPrefs.DeleteKey("Map");
        ScenesManager.Instance.LoadNewGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
