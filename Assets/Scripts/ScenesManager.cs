using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            this.tag = "SceneManager";
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum Scene
    {
        MainMenuScene,
        MapScene,
        CombatScene,
        RestScene,
        ShopScene,
        TreasureScene,
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadMapScene()
    {
        MapPlayerTracker.Instance.Locked = false;
        GameManager.instance.SaveGame();
        SceneManager.LoadScene(Scene.MapScene.ToString());
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene.MapScene.ToString());
    }

    public void LoadCombatScene()
    {
        SceneManager.LoadScene(Scene.CombatScene.ToString());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenuScene.ToString());
    }

    public void WinStage()
    {
        var currentDifficulty = GameManager.instance._playerInventory.difficulty;
        if (PlayerPrefs.HasKey("HighestDifficulty"))
        {
            if (currentDifficulty < PlayerPrefs.GetFloat("HighestDifficulty"))
            {
                currentDifficulty = PlayerPrefs.GetFloat("HighestDifficulty");
            }
        }

        PlayerPrefs.SetFloat("HighestDifficulty", currentDifficulty);

        SceneManager.LoadScene(Scene.MainMenuScene.ToString());
    }
}
