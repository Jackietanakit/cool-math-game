using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ASyncLoader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField]
    private GameObject loadingScreen;

    [SerializeField]
    private GameObject mainMenu;

    [Header("Slider")]
    private Slider loadingSlider;

    public void LoadLevelBtn()
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(
            ScenesManager.Scene.MapScene.ToString()
        );

        while (!asyncLoad.isDone)
        {
            loadingSlider.value = (float)Mathf.Clamp01(asyncLoad.progress / 0.9f);
            yield return null;
        }
    }
}
