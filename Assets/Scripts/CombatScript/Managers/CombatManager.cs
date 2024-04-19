using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    public NumberBlockZone NumberBlockZone;

    [SerializeField]
    SPUM_Prefabs mainCharacterPrefab;

    [SerializeField]
    Transform mainCharacterPosition;

    public int PlayerHealth;

    public Slider HealthSlider;

    public Enemy enemyPrefab;

    public List<EnemyInfoSO> enemyInfoSOs;

    public GameObject EnemyContainer; //The parent of the enemies

    public DamageZone damageZone;

    public bool hasDraggedSomething;

    public DamageButton damageButton;

    public Enemy[] enemiesInScene;

    public List<Transform> enemiesPossiblePositions;

    public Transform StartingPosition;

    public List<Zone> zones;

    public PlayerInventory playerInventory;

    public List<EnemyInfoSO> enemiesThisCombat;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        //if there is no Gamemanager, then create 4 example enemies
        if (playerInventory == null)
        {
            enemiesThisCombat = new List<EnemyInfoSO>
            {
                enemyInfoSOs[0],
                enemyInfoSOs[0],
                enemyInfoSOs[0],
                enemyInfoSOs[0],
            };
        }
        else
        {
            //Get the enemies from Gamemanager
        }
        InititializeFromPlayerInventory();
        NumberBlocksManager.Instance.CreateManyNumberBlocks(
            NumberBlocksManager.Instance.GenerateStartingRandomFairNumbers(
                NumberBlocksManager.Instance.numberSpawnPerTurn
            )
        );
        OperatorBlockManager.Instance.CreateManyOperators(
            new List<OperationName>
            {
                OperationName.Add,
                OperationName.Subtract,
                OperationName.Multiply,
                OperationName.Divide,
            }
        );
        enemiesInScene = new Enemy[4] { null, null, null, null };
        SpawnNewEnemy();
    }

    private void InititializeFromPlayerInventory()
    {
        if (playerInventory != null)
        {
            PlayerHealth = playerInventory.currentHealth;
        }
        else
        {
            PlayerHealth = (int)HealthSlider.maxValue;
        }
    }

    private List<OperationCard> operationCards = new List<OperationCard>();

    void Awake()
    {
        Instance = this;
    }

    public void SpawnNewEnemy()
    {
        //if there is an enemy at the first position, don't spawn a new one
        if (enemiesInScene[0] != null || enemiesThisCombat.Count == 0)
        {
            return;
        }
        //pull the enemyinfo from the enemiesthsicombat
        //create enemy
        EnemyInfoSO enemyInfo = enemiesThisCombat[0];
        CreateEnemyAtFirstPosition(enemyInfo);
        enemiesThisCombat.RemoveAt(0);
    }

    public void CreateEnemyAtFirstPosition(EnemyInfoSO enemyInfoSO)
    {
        //Instantiate enemy at position 0
        Enemy enemy = Instantiate(enemyPrefab, StartingPosition, true);
        enemy.transform.localPosition = new Vector3(0, 0.1f, 0);
        //Move to enemypossiblepostion[0] using dotween
        enemy.transform.DOMove(
            enemiesPossiblePositions[0].position + new Vector3(0, 0.5f, 0),
            0.5f
        );
        enemy.Initialize(enemyInfoSOs[0]);
        enemiesInScene[0] = enemy;
        Debug.Log("Enemy created");
    }

    public Block GetBlockUnderMouse()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            Camera.main.ScreenToWorldPoint(Input.mousePosition),
            Vector2.zero
        );
        if (hit.collider != null)
        {
            return hit.collider.GetComponent<Block>();
        }
        return null;
    }

    public Zone GetZoneUnderMouse()
    {
        foreach (Zone zone in zones)
        {
            if (zone.IsInZone(GetBlockUnderMouse()))
            {
                return zone;
            }
        }
        return null;
    }

    public void DealDamage()
    {
        if (damageZone.numbers.Count != 0)
        {
            NumberBlock numberBlock = damageZone.numbers[0];
            int damage = numberBlock.number;
            NumberBlocksManager.Instance.RemoveNumberBlockFromList(numberBlock);
            numberBlock.RemoveBlock();
            damageZone.RemoveBlockFromZone(numberBlock);
            //Damage Enemy at the closest distance to the player, using enemies array, [0] is furthest distance
            //Get closest enemy
            // Get closest enemy
            Enemy closestEnemy = null;
            for (int i = enemiesInScene.Length - 1; i >= 0; i--)
            {
                if (enemiesInScene[i] != null)
                {
                    closestEnemy = enemiesInScene[i];
                    break;
                }
            }

            if (closestEnemy != null)
            {
                bool isDead = closestEnemy.TakeDamage(damage);
                if (isDead)
                {
                    //remove from the array
                    for (int i = 0; i < enemiesInScene.Length; i++)
                    {
                        if (enemiesInScene[i] == closestEnemy)
                        {
                            enemiesInScene[i] = null;
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("No Enemy in zone");
            }

            Debug.Log("Dealing " + damage + " damage");

            NextTurn();
        }
        else
        {
            Debug.Log("No Number in zone");
        }
    }

    public void NextTurn()
    {
        //if there is no enemy in the list and in the scene then the player wins
        if (
            enemiesThisCombat.Count == 0
            && enemiesInScene[0] == null
            && enemiesInScene[1] == null
            && enemiesInScene[2] == null
            && enemiesInScene[3] == null
        )
        {
            Debug.Log("Player wins");
            //Player wins, shows a panel

            return;
        }
        NumberBlocksManager.Instance.NextTurn();
        CalculationManager.Instance.ClearAll();
        MoveEnemiesForward();
        SpawnNewEnemy();
    }

    public void MoveEnemiesForward()
    {
        for (int i = enemiesInScene.Length - 1; i >= 0; i--)
        {
            if (enemiesInScene[i] != null)
            {
                if (i + 1 < enemiesPossiblePositions.Count && enemiesInScene[i + 1] == null)
                {
                    enemiesInScene[i]
                        .transform.DOMove(
                            enemiesPossiblePositions[i + 1].position + new Vector3(0, 0.5f, 0),
                            0.5f
                        );
                    enemiesInScene[i + 1] = enemiesInScene[i];
                    enemiesInScene[i] = null;
                }
                else if (i == enemiesInScene.Length - 1 && enemiesInScene[i] != null)
                {
                    //Enemies reached the player
                    Debug.Log("Enemies reached the player");
                    PlayerTakeDamage();
                }
            }
        }
    }

    public void PlayerTakeDamage()
    {
        PlayerHealth -= 1;
        HealthSlider.value = PlayerHealth;
        if (PlayerHealth <= 0)
        {
            //Player dies
            Debug.Log("Player dies");

            //Game Over Panel

            //
        }
    }
}
