using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        ScenesManager.Instance.LoadNewGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
