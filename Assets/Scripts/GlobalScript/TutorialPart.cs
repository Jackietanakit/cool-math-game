using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TutorialPart : MonoBehaviour
{
    // Start is called before the first frame update    public TutorialType tutorialType

    public TutorialPartInfo tutorialPartInfo;
    public Transform maskPosition;

    public Transform panelPosition;
    public TutorialType tutorialType;
    public bool hasShown;

    public bool needMask;
    public int order;
    public TextMeshPro tutorialTitle;
    public TextMeshPro tutorialText;
    public GameObject tutorialPanel;
    public BoxCollider2D PreviousTutorialButton;
    public BoxCollider2D NextTutorialButton;

    public SpriteMask spriteMask;

    public GameObject mask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (
                PreviousTutorialButton.OverlapPoint(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition)
                )
            )
            {
                TutorialManager.Instance.ShowPreviousTutorial(this);
            }
            else if (
                NextTutorialButton.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))
            )
            {
                TutorialManager.Instance.ShowNextTutorial(this);
            }
        }
    }

    public void Init(TutorialPartInfo tutorialPartInfo)
    {
        this.transform.position = new Vector3(0, 0, 0);
        this.tutorialPartInfo = tutorialPartInfo;
        this.maskPosition.position = tutorialPartInfo.maskPosition;
        this.spriteMask.transform.localScale = tutorialPartInfo.maskScale;
        this.panelPosition.position = tutorialPartInfo.panelPosition;
        this.tutorialType = tutorialPartInfo.tutorialType;
        this.tutorialTitle.text = tutorialPartInfo.tutorialTitle;
        this.tutorialText.text = tutorialPartInfo.tutorialText;
        this.needMask = tutorialPartInfo.needMask;
        this.order = tutorialPartInfo.order;

        //If the tutorial is the last or the first tutorial, set the button inactive
        if (tutorialPartInfo.order == 0)
        {
            PreviousTutorialButton.gameObject.SetActive(false);
        }
        if (
            tutorialPartInfo.order
            == TutorialManager.Instance.GetTutorialPartCount(tutorialPartInfo.tutorialType) - 1
        )
        {
            NextTutorialButton.gameObject.SetActive(false);
        }

        if (!needMask)
        {
            mask.SetActive(false);
        }
    }

    public void HideTutorial()
    {
        // Hide the tutorial panel
        GameObject.Destroy(this.gameObject);
    }
}

public enum TutorialType
{
    Map,
    Combat,
    Shop,
    Rest,
}
