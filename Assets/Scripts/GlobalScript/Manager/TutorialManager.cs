using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public TutorialPart TutorialPartPrefab;
    public bool WantMapTutorial;
    public List<TutorialPartInfo> MapTutorials;
    public List<TutorialPartInfo> CombatTutorials;

    public GameObject AskCombatTutorialPanel; // Ask whether the player wants to see the Combat tutorials
    public List<EnemyInfoSO> TutorialEnemyInfos;

    public List<OperationCard> TutorialOperationCards;

    public bool IsInCombatTutorial;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure GameManager persists between scenes if needed
        }
        else
        {
            Destroy(gameObject); // Ensure only one GameManager instance exists
        }
    }

    public void StartMapTutorial() { }

    public void LoadCombatTutorialScene()
    {
        //Start Combat tutorial, Load combat tutorials, Load combat Scene
        IsInCombatTutorial = true;
        //Load CombatScene
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.CombatScene);

        //Load Combat Tutorials
        StartCombatTutorials();
    }

    private void StartCombatTutorials()
    {
        // Start the first tutorial, Initiate using combat tutorials data
        ShowTutorial(CombatTutorials[0]);
    }

    public TutorialPartInfo GetPreviousTutorialPart(TutorialPartInfo tutorialPart)
    {
        TutorialPartInfo previousTutorialPart = new TutorialPartInfo();
        switch (tutorialPart.tutorialType)
        {
            case TutorialType.Map:
                previousTutorialPart = MapTutorials.Find(x => x.order == tutorialPart.order - 1);
                break;
            case TutorialType.Combat:
                previousTutorialPart = CombatTutorials.Find(x => x.order == tutorialPart.order - 1);
                break;
        }

        return previousTutorialPart;
    }

    public TutorialPartInfo GetNextTutorialPart(TutorialPartInfo tutorialPart)
    {
        TutorialPartInfo nextTutorialPart = new TutorialPartInfo();
        switch (tutorialPart.tutorialType)
        {
            case TutorialType.Map:
                nextTutorialPart = MapTutorials.Find(x => x.order == tutorialPart.order + 1);
                break;
            case TutorialType.Combat:
                nextTutorialPart = CombatTutorials.Find(x => x.order == tutorialPart.order + 1);
                break;
        }

        return nextTutorialPart;
    }

    private void ShowTutorial(TutorialPartInfo tutorialPartInfo)
    {
        Debug.Log("Tutorial " + tutorialPartInfo.order + " is shown");
        TutorialPart tutorialPart = Instantiate(TutorialPartPrefab, transform, true);
        tutorialPart.Init(tutorialPartInfo);
    }

    public void ShowPreviousTutorial(TutorialPart tutorialPart)
    {
        TutorialPartInfo previousTutorialPart = GetPreviousTutorialPart(
            tutorialPart.tutorialPartInfo
        );
        if (previousTutorialPart.Equals(new TutorialPartInfo()))
        {
            // End of tutorial
            return;
        }
        ShowTutorial(previousTutorialPart);

        //hide the previous tutorial
        tutorialPart.HideTutorial();
    }

    public void ShowNextTutorial(TutorialPart tutorialPart)
    {
        TutorialPartInfo nextTutorialPart = GetNextTutorialPart(tutorialPart.tutorialPartInfo);
        if (nextTutorialPart.Equals(new TutorialPartInfo()))
        {
            // End of tutorial
            return;
        }
        ShowTutorial(nextTutorialPart);

        //hide the previous tutorial
        tutorialPart.HideTutorial();
    }

    public int GetTutorialPartCount(TutorialType tutorialType)
    {
        switch (tutorialType)
        {
            case TutorialType.Map:
                return MapTutorials.Count;
            case TutorialType.Combat:
                return CombatTutorials.Count;
            default:
                return 0;
        }
    }
}

[Serializable]
public struct TutorialPartInfo
{
    public Vector2 maskPosition;

    public Vector2 maskScale;
    public Vector2 panelPosition;
    public TutorialType tutorialType;
    public string tutorialTitle;
    public string tutorialText;
    public bool needMask;
    public int order;
}
