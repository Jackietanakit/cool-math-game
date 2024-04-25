using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerInventory _playerInventory;

    public AllStaticData allStaticData;

    public ScenesManager scenesManager;

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
        scenesManager = FindObjectOfType<ScenesManager>();
    }

    private void InitializePlayerInventory()
    {
        _playerInventory.CreateNewPlayerInventory(); // Reset the player's inventory
    }

    public List<EnemyInfoSO> GenerateEnemies()
    {
        // Generate enemies
        List<EnemyInfoSO> enemies = new List<EnemyInfoSO>();
        for (int i = 0; i < Random.Range(4, 6); i++)
        {
            enemies.Add(
                allStaticData.enemyInfoSOs[Random.Range(0, allStaticData.enemyInfoSOs.Count)]
            );
        }
        return enemies;
    }
}
