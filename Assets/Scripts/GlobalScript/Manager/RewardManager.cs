using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;
    public static PlayerInventory playerInventory;
    public OperationCard newCard;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public void HealHealth(int amount)
    {
        playerInventory.currentHealth += amount;
    }

    public void AddMoney(int amount)
    {
        playerInventory.money += amount;
    }

    public void AddOperationCard()
    {
        playerInventory.AddOperationCard(newCard);
    }

    public void AddArtifact(Artifact artifact)
    {
        playerInventory.AddArtifact(artifact);
    }

    public void RemoveOperationCard(OperationCard card)
    {
        playerInventory.RemoveOperationCard(card);
    }
}
