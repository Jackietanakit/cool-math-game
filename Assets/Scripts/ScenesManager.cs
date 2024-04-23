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
        TreasureScene,
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadMapScene()
    {
        MapPlayerTracker.Instance.Locked = false;
        SceneManager.LoadScene(Scene.MapScene.ToString());
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene.MapScene.ToString());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenuScene.ToString());
    }
}
