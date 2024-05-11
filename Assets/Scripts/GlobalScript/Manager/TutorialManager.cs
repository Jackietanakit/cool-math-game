using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    public bool WantMapTutorial;
    public List<TutorialPart> MapTutorials;
    public List<TutorialPart> CombatTutorials;

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

    public void StartCombatTutorial()
    {
        //Start Combat tutorial, Load combat tutorials, Load combat Scene
        IsInCombatTutorial = true;
        //Load CombatScene
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.CombatScene);
    }

    public TutorialPart GetPreviousTutorialPart(TutorialPart tutorialPart)
    {
        TutorialPart previousTutorialPart = new TutorialPart();
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

    public TutorialPart GetNextTutorialPart(TutorialPart tutorialPart)
    {
        TutorialPart nextTutorialPart = new TutorialPart();
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

    public void ShowNextTutorial(TutorialPart tutorialPart)
    {
        TutorialPart nextTutorialPart = GetNextTutorialPart(tutorialPart);
        if (nextTutorialPart.Equals(new TutorialPart()))
        {
            // End of tutorial
            return;
        }

        if (nextTutorialPart.hasShown)
        {
            ShowNextTutorial(nextTutorialPart);
        }
        else
        {
            nextTutorialPart.ShowTutorial();
        }
    }
}

[Serializable]
public struct TutorialPart
{
    public TutorialType tutorialType;
    public bool hasShown;
    public int order;
    public GameObject tutorialPanel;

    public void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
        hasShown = true;
    }

    public void HideTutorial()
    {
        tutorialPanel.SetActive(false);
    }
}

public enum TutorialType
{
    Map,
    Combat,
    Shop,
    Rest,
}
