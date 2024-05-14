using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactGrid : MonoBehaviour
{
    public Artifact ArtifactInfo;

    public UnityEngine.UI.Image ArtifactImage;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI PriceText;

    public bool hasInteracted;
    public Button BuyButton;

    void Update()
    {
        if (GameManager.instance._playerInventory.money > ArtifactInfo.price && !hasInteracted)
        {
            BuyButton.interactable = true;
        }
        else
        {
            BuyButton.interactable = false;
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
