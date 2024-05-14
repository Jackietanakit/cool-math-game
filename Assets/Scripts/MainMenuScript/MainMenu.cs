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
        PlayerPrefs.DeleteKey("Tutorial");
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
                    "Initial Difficulty: 1.2"
                    + "\nStarting Coin: 100"
                    + "\nReaching 1.5 Difficulty"
                    + "\nNormal Mode!";
                break;
            case "Hard":
                titleText.text = "Hard";
                informationText.text =
                    "Initial Difficulty: 1.5"
                    + "\nStarting Coin: 50"
                    + "\nReaching 1.8 Difficulty"
                    + "\nHard Mode!";
                break;
            case "Challenging":
                titleText.text = "Challenging";
                informationText.text =
                    "Initial Difficulty: 1.8"
                    + "\nStarting Coin: 0"
                    + "\nReaching 2.1 Difficulty"
                    + "\nChallenging Mode!";
                break;
            case "Extreme":
                titleText.text = "Extreme";
                informationText.text =
                    "Initial Difficulty: 2"
                    + "\nStarting Coin: 0"
                    + "\nReaching 3 Difficulty"
                    + "\nExtreme Mode!";
                break;
            default:
                break;
        }
    }

    public void Test()
    {
        PlayerPrefs.SetFloat("HighestDifficulty", 1.5f);
    }

    public void SetDisableDifficulty()
    {
        if (HighestDifficulty < 1.5f)
        {
            NormalButton.interactable = false;
            NormalText.text = "Locked";
        }
        if (HighestDifficulty < 1.8f)
        {
            HardButton.interactable = false;
            HardText.text = "Locked";
        }
        if (HighestDifficulty < 2.1f)
        {
            ChallengingButton.interactable = false;
            ChallengingText.text = "Locked";
        }
        if (HighestDifficulty < 3f)
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

    public void NewGame(float difficulty)
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
        GameManager.instance.InitializePlayerInventory(1.0f);
        GameManager.instance.IsNewGame = true;
        PlayerPrefs.DeleteKey("Map");
        ScenesManager.Instance.LoadNewGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
