using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerInventory _playerInventory;

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
        _playerInventory.CreateNewPlayerInventory(); // Reset the player's inventory
    }
}
