using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensure GameManager persists between scenes if needed
        }
        else
        {
            Destroy(gameObject); // Ensure only one GameManager instance exists
        }
    }

    private void Start()
    {
        InitializePlayerInventory(); // Initialize the player's inventory
    }

    private void InitializePlayerInventory()
    {
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();

        if (playerInventory == null)
        {
            playerInventory = gameObject.AddComponent<PlayerInventory>(); // Create the PlayerInventory component if not already present
        }
        playerInventory.createNewPlayerInventory(); // Reset the player's inventory
    }
}
