using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;
    public static PlayerInventory playerInventory;

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

    public void AddOperationCard(OperationCard card)
    {
        playerInventory.AddOperationCard(card);
    }

    public void AddArtifact(Artifact artifact)
    {
        playerInventory.AddArtifact(artifact);
    }
}
