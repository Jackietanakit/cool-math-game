using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.TerrainUtils;
using UnityEngine.UI;

public class ArtifactGrid : MonoBehaviour
{
    public Artifact ArtifactInfo;

    public UnityEngine.UI.Image ArtifactImage;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI PriceText;

    public CanvasGroup descriptionPanel;

    public TextMeshProUGUI DescriptionText;

    public BoxCollider2D artifactcollider;

    public bool hasInteracted;
    public Button BuyButton;

    void Update()
    {
        //If the enemy is hovered for 0.5 seconds, show the panel. if not stop showing. The panel fade out and in
        if (artifactcollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            StartCoroutine(ShowPanel());
        }
        else
        {
            StartCoroutine(FadeOutPanel());
        }

        if (GameManager.instance._playerInventory.money > ArtifactInfo.price && !hasInteracted)
        {
            BuyButton.interactable = true;
        }
        else
        {
            BuyButton.interactable = false;
        }
    }

    private IEnumerator ShowPanel()
    {
        //if the enemy is hovered for 0.5 seconds, show the panel
        yield return new WaitForSeconds(0.5f);
        if (artifactcollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            StartCoroutine(FadeInPanel());
        }
    }

    private IEnumerator FadeInPanel()
    {
        while (descriptionPanel.alpha < 1)
        {
            descriptionPanel.alpha += Time.deltaTime / 2;
            yield return null;
        }
    }

    private IEnumerator FadeOutPanel()
    {
        while (descriptionPanel.alpha > 0)
        {
            descriptionPanel.alpha -= Time.deltaTime / 2;
            yield return null;
        }
    }

    public void Initialize(Artifact ArtifactInfo)
    {
        this.ArtifactInfo = ArtifactInfo;
        SetArtifact(ArtifactInfo.ArtifactName);
    }

    public void SetArtifact(string ArtifactName)
    {
        ArtifactImage.sprite = ArtifactInfo.artifactSprite;
        NameText.text = ArtifactInfo.ArtifactName;
        PriceText.text = ShopManager.Instance.IsInBuyShop
            ? ArtifactInfo.price.ToString()
            : (ArtifactInfo.price / 2).ToString();
        DescriptionText.text = ArtifactInfo.description;
    }

    public void BuyOrSellArtifact()
    {
        if (ShopManager.Instance.IsInBuyShop)
        {
            RewardManager.Instance.AddArtifact(ArtifactInfo);
            RewardManager.Instance.RemoveMoney(ArtifactInfo.price);
        }
        else
        {
            GameManager.instance._playerInventory.RemoveArtifact(ArtifactInfo);
            RewardManager.Instance.AddMoney(ArtifactInfo.price / 2);
        }
        hasInteracted = true;
        ShopManager.Instance.UpdateUI();
    }
}
