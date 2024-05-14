using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerInventory _playerInventory;

    public bool IsInBoss;

    public bool IsInElite;

    public AllStaticData allStaticData;

    public ScenesManager scenesManager;
    public bool IsNewGame;

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

    private void Start() { }

    public void InitializePlayerInventory(float difficulty)
    {
        _playerInventory.CreateNewPlayerInventory(); // Reset the player's inventory
        _playerInventory.difficulty = difficulty;
    }

    public List<EnemyInfoSO> GenerateEnemies()
    {
        List<EnemyInfoSO> enemies = new List<EnemyInfoSO>();

        // Randomly pick an enemy group from the mapid = 1
        List<EnemySpawningPatternInfo> enemyGroups = allStaticData.EnemySpawnInfos.FindAll(x =>
            x.mapid == 1
        );
        if (GameManager.instance.IsInBoss)
        {
            enemyGroups = allStaticData.EnemySpawnInfos.FindAll(x => x.mapid == 2);
        }
        else if (GameManager.instance.IsInElite)
        {
            enemyGroups = allStaticData.EnemySpawnInfos.FindAll(x => x.mapid == 1);
        }
        EnemySpawningPatternInfo enemySpawningPatternInfo = enemyGroups[
            UnityEngine.Random.Range(0, enemyGroups.Count)
        ];
        // Depending on the type of enemy spawning pattern, generate the enemies
        switch (enemySpawningPatternInfo.type)
        {
            case EnemySpawnType.Fixed:
                enemies = enemySpawningPatternInfo.Enemies;
                break;
            case EnemySpawnType.Random:
                for (int i = 0; i < enemySpawningPatternInfo.amountEnemySpawn; i++)
                {
                    enemies.Add(
                        enemySpawningPatternInfo.Enemies[
                            UnityEngine.Random.Range(0, enemySpawningPatternInfo.Enemies.Count)
                        ]
                    );
                }
                break;
            case EnemySpawnType.Repeating:
                for (int i = 0; i < enemySpawningPatternInfo.amountEnemySpawn; i++)
                {
                    enemies.Add(
                        enemySpawningPatternInfo.Enemies[i % enemySpawningPatternInfo.Enemies.Count]
                    );
                }
                break;
        }

        return enemies;
    }

    public void SaveGame()
    {
        _playerInventory.SaveInventory();
    }
}
