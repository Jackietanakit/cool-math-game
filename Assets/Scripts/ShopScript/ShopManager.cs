using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    public List<Artifact> ArtifactsInBuyShop;

    public List<Artifact> ArtifactsInSellShop;

    public Slider HealthSlider;

    public TextMeshProUGUI MoneyText;

    public bool IsInBuyShop;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
        SetArtifactsInBuyShop();
        SetArtifactsInSellShop();
        SwitchToBuyArtifacts();
    }

    public void SwitchToBuyArtifacts()
    {
        IsInBuyShop = true;
        GridManager.Instance.SwitchToBuyArtifact();
    }

    public void SwitchToSellArtifacts()
    {
        IsInBuyShop = false;
        GridManager.Instance.SwitchToSellArtifact();
    }

    void SetArtifactsInBuyShop()
    {
        //Get a random list of 3 artifacts from allstaticdata in game manager, the list can't have the artifacts that the player has
        List<Artifact> playerArtifacts = GameManager.instance._playerInventory.artifacts;
        List<Artifact> availableArtifacts = GameManager.instance.allStaticData.artifacts;

        List<Artifact> artifactsInBuyShop = new List<Artifact>();

        while (
            artifactsInBuyShop.Count < 3
            && artifactsInBuyShop.Count + playerArtifacts.Count < availableArtifacts.Count
        )
        {
            Artifact randomArtifact = availableArtifacts[Random.Range(0, availableArtifacts.Count)];
            if (
                !playerArtifacts.Contains(randomArtifact)
                && !artifactsInBuyShop.Contains(randomArtifact)
            )
            {
                artifactsInBuyShop.Add(randomArtifact);
            }
        }

        ArtifactsInBuyShop = artifactsInBuyShop;
    }

    void SetArtifactsInSellShop()
    {
        //Get a random list of 3 artifacts from the player's inventory
        List<Artifact> playerArtifacts = GameManager.instance._playerInventory.artifacts;

        List<Artifact> artifactsInSellShop = new List<Artifact>();

        while (artifactsInSellShop.Count < 2 && artifactsInSellShop.Count < playerArtifacts.Count)
        {
            Artifact randomArtifact = playerArtifacts[Random.Range(0, playerArtifacts.Count)];
            if (!artifactsInSellShop.Contains(randomArtifact))
            {
                artifactsInSellShop.Add(randomArtifact);
            }
        }

        ArtifactsInSellShop = artifactsInSellShop;
    }

    public void UpdateUI()
    {
        HealthSlider.value = GameManager.instance._playerInventory.currentHealth;
        MoneyText.text = GameManager.instance._playerInventory.money.ToString();
    }

    public void LeaveScene()
    {
        ScenesManager.Instance.LoadMapScene();
    }
}
