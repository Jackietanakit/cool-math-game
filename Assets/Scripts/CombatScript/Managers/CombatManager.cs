using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    public GameState currentState = GameState.ArtifactInitiation;
    public NumberBlockZone NumberBlockZone;

    public VictoryPanel victoryPanel;

    public GameObject defeatPanel;

    public FinalCombatInfo finalcombatInfo; //Contains the final combat info

    public CombatPositionsAndPrefabs initCombatInfo; //Contain all prefab and position

    public int PlayerHealth;
    public Slider HealthSlider;
    public DamageZone damageZone;

    public bool hasDraggedSomething;

    public NumberBlock DraggedNumberBlock;

    public DamageButton damageButton;

    public TextMeshProUGUI moneyText;

    public Enemy[] enemiesInScene;

    public List<Zone> zones;

    public List<EnemyInfoSO> enemiesThisCombat;

    void Start()
    {
        Instance.ChangeGameState(GameState.CombatInitialization);
    }

    void Awake()
    {
        Instance = this;
    }

    public void ChangeGameState(GameState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case GameState.CombatInitialization:
                Debug.Log("Combat Initialization");
                InitializeCombat();
                CombatManager.Instance.ChangeGameState(GameState.ArtifactInitiation);
                break;
            case GameState.ArtifactInitiation:
                Debug.Log("Artifact Initiation");
                ArtifactManager.Instance.OnceStartCombat();
                CombatManager.Instance.ChangeGameState(GameState.BeforePlayerTurn);
                break;
            case GameState.BeforePlayerTurn:
                Debug.Log("Before Player Turn");
                NumberBlocksManager.Instance.NextTurn(); // Create number blocks
                CalculationManager.Instance.ClearAll(); // Clear all number blocks
                ArtifactManager.Instance.BeforeTurnStart();
                CombatManager.Instance.ChangeGameState(GameState.PlayerTurn);
                break;
            case GameState.PlayerTurn:
                Debug.Log("Player Turn");
                //This state allows the player to move the number blocks and calculate
                break;
            case GameState.AfterPlayerTurn:
                Debug.Log("After Player Turn");
                //Calculate the damage
                DamageInfo damageInfo = new DamageInfo(
                    damageZone.numbers[0].number,
                    IsPerfect(damageZone.numbers[0].number)
                );
                damageInfo = ArtifactManager.Instance.AfterTurn(damageInfo);

                DealDamage(damageInfo);
                CombatManager.Instance.ChangeGameState(GameState.EnemyTurn);
                break;
            case GameState.EnemyTurn:
                NextTurn();
                EnemyTurn();
                Debug.Log("Enemy Turn");
                if (currentState != GameState.Waiting)
                {
                    CombatManager.Instance.ChangeGameState(GameState.BeforePlayerTurn);
                }
                break;
            case GameState.Waiting:
                Debug.Log("Waiting");
                break;
        }
    }

    private void InitializeCombat()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            //throw error exception manager not found
            Debug.LogError("GameManager not found");
        }
        else
        {
            //Get the enemies from Gamemanager
            if (TutorialManager.Instance.IsInCombatTutorial)
            {
                enemiesThisCombat = TutorialManager.Instance.TutorialEnemyInfos;
                OperatorBlockManager.Instance.CreateManyOperators(
                    TutorialManager.Instance.TutorialOperationCards
                );
                TutorialManager.Instance.IsInCombatTutorial = false;
            }
            else
            {
                enemiesThisCombat = GameManager.instance.GenerateEnemies();
                //load
                InititializeFromPlayerInventory();
            }
        }

        enemiesInScene = new Enemy[4] { null, null, null, null };
        SpawnNewEnemy();
    }

    private void InititializeFromPlayerInventory()
    {
        if (GameManager.instance._playerInventory != null)
        {
            PlayerHealth = GameManager.instance._playerInventory.currentHealth;
        }
        else
        {
            PlayerHealth = (int)HealthSlider.maxValue;
        }

        OperatorBlockManager.Instance.CreateManyOperators(
            GameManager.instance._playerInventory.operationCards
        );

        moneyText.text = GameManager.instance._playerInventory.money.ToString();
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
        //Instantiate enemy from prefab of the enemy
        Enemy enemy = Instantiate(
            initCombatInfo.enemyPrefab,
            initCombatInfo.StartingPosition.position,
            Quaternion.identity
        );

        //Move to enemypossiblepostion[0] using dotween
        enemy.transform.DOMove(initCombatInfo.enemiesPossiblePositions[0].position, 0.5f);
        enemy.Initialize(enemyInfoSO);
        enemiesInScene[0] = enemy;
        Debug.Log("Enemy created");
    }

    public Block GetBlockUnderMouse()
    {
        return DraggedNumberBlock;
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

    public void DealDamage(DamageInfo damageInfo)
    {
        if (damageZone.numbers.Count != 0)
        {
            NumberBlock numberBlock = damageZone.numbers[0];
            int damage = numberBlock.number;
            NumberBlocksManager.Instance.RemoveNumberBlockFromList(numberBlock);
            numberBlock.RemoveBlock();
            damageZone.RemoveBlockFromZone(numberBlock);
            //Damage Enemy at the closest distance to the player, using enemies array, [0] is furthest distance\
            //Damage can have number of piercing eg. if piercing = 1 then 2 closest enemies will receive damage

            // Get closest enemies based on the number of piercing
            List<Enemy> closestEnemies = GetNearestEnemies(damageInfo.piercing + 1);
            foreach (Enemy enemy in closestEnemies)
            {
                bool isDead = enemy.TakeDamage(damage);
                Debug.Log("Dealing " + damage + " damage");
                if (isDead)
                {
                    // Remove from the array
                    for (int i = 0; i < enemiesInScene.Length; i++)
                    {
                        if (enemiesInScene[i] == enemy)
                        {
                            enemiesInScene[i] = null;
                            break;
                        }
                    }

                    // Update combat info
                    finalcombatInfo.enemiesDefeated += 1;
                }
            }
        }
        else
        {
            Debug.Log("No Number in zone");
        }
    }

    public List<Enemy> GetNearestEnemies(int numberOfEnemy)
    {
        //Get the n enemy closest to the player
        List<Enemy> nearestEnemies = new List<Enemy>();
        for (int i = enemiesInScene.Length - 1; i >= 0; i--)
        {
            if (enemiesInScene[i] != null)
            {
                nearestEnemies.Add(enemiesInScene[i]);
                if (nearestEnemies.Count == numberOfEnemy)
                {
                    break;
                }
            }
        }
        return nearestEnemies;
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
            Win();
            return;
        }
    }

    public void EnemyTurn()
    {
        MoveEnemiesForward();
        SpawnNewEnemy();
    }

    public void MoveEnemiesForward()
    {
        for (int i = enemiesInScene.Length - 1; i >= 0; i--)
        {
            if (enemiesInScene[i] != null)
            {
                if (
                    i + 1 < initCombatInfo.enemiesPossiblePositions.Count
                    && enemiesInScene[i + 1] == null
                )
                {
                    enemiesInScene[i]
                        .transform.DOMove(
                            initCombatInfo.enemiesPossiblePositions[i + 1].position,
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

    bool IsPerfect(int Damage)
    {
        //check if the damage is equal to the enemy health and all numberblocks are used
        bool isPerfect =
            Damage == GetNearestEnemies(1)[0].health
            && NumberBlocksManager.Instance.numberBlocksInContainer == 0;
        if (isPerfect)
        {
            finalcombatInfo.perfect += 1;
            Debug.Log("Perfect!");
        }
        return isPerfect;
    }

    public void PlayerTakeDamage()
    {
        PlayerHealth -= 1;
        HealthSlider.value = PlayerHealth;
        if (PlayerHealth <= 0)
        {
            Lose();
        }
    }

    public void ChangeScene(string scenename)
    {
        if (GameManager.instance.IsInBoss)
        {
            ScenesManager.Instance.LoadMainMenu();
        }
        else
        {
            SceneManager.LoadScene(scenename);
        }
    }

    public void Win()
    {
        TutorialManager.Instance.EndTutorial();
        UpdateInventory();
        victoryPanel.ShowPanel(finalcombatInfo.ToString());
        Instance.ChangeGameState(GameState.Waiting);

        Debug.Log("Player wins");

        if (GameManager.instance.IsInBoss)
        {
            ScenesManager.Instance.WinStage();
        }
        //Player wins, shows a panel
    }

    public void Lose()
    {
        //Player loses, shows a panel
        defeatPanel.SetActive(true);
        GameManager.instance._playerInventory.CreateNewPlayerInventory();
        Instance.ChangeGameState(GameState.Waiting);
    }

    void UpdateInventory()
    {
        // Update Difficulty  = 𝑃 + 𝑚𝑎𝑥(0. 1 × 𝐷𝑖𝑓𝑀𝑜𝑑𝑖𝑓𝑖𝑒𝑟 , 0. 1(− 2 × 𝐻𝑒𝑎𝑙𝑡ℎ𝑙𝑜𝑠𝑡 + 𝑃𝑒𝑟𝑓𝑒𝑐𝑡 + 𝐷𝑖𝑓𝑀𝑜𝑑𝑖𝑓𝑖𝑒𝑟 + 2 × 𝐼𝑠𝐸𝑙𝑖𝑡𝑒 + 1)
        float increasedDiff = Math.Max(
            0.1f,
            0.1f
                * (
                    -2 * finalcombatInfo.damageTaken
                    + finalcombatInfo.perfect
                    + GameManager.instance._playerInventory.difficultyModifier
                    + 2 * (finalcombatInfo.isElite ? 1 : 0)
                    + 1
                )
        );

        GameManager.instance._playerInventory.difficulty += increasedDiff;
        finalcombatInfo.difficultyAdded = increasedDiff;
        finalcombatInfo.newDifficulty = GameManager.instance._playerInventory.difficulty;

        //Update Coin -> enemy defeated * 10 + perfect * 10
        int coinGained = finalcombatInfo.enemiesDefeated * 10 + finalcombatInfo.perfect * 10;
        finalcombatInfo.coinGained = coinGained;
        GameManager.instance._playerInventory.money += coinGained;
    }

    //FOR DEMO PURPOSE ONLY
    public void KillFirstEnemy()
    {
        var nearestEnemies = GetNearestEnemies(1)[0];
        //set the element in the array to null
        for (int i = 0; i < enemiesInScene.Length; i++)
        {
            if (enemiesInScene[i] == nearestEnemies)
            {
                enemiesInScene[i] = null;
                break;
            }
        }

        nearestEnemies.Die();
        ChangeGameState(GameState.EnemyTurn);
    }
}

public struct FinalCombatInfo
{
    public int enemiesDefeated;
    public int perfect;
    public int damageTaken;
    public int coinGained;

    public bool isElite;
    public float difficultyAdded;
    public float newDifficulty;

    public FinalCombatInfo(bool isElite)
    {
        this.enemiesDefeated = 0;
        this.perfect = 0;
        this.isElite = isElite;
        this.damageTaken = 0;
        this.coinGained = 0;
        this.difficultyAdded = 0;
        this.newDifficulty = 0;
    }

    public override string ToString()
    {
        return String.Format(
            "Enemies Defeated :  {0}"
                + "\nPerfect : {1}"
                + "\nDamage Taken : {2}"
                + "\nCoin gained : {3}"
                + "\nDifficulty Added: +{4}"
                + "\nNew Difficulty: {5}",
            enemiesDefeated,
            perfect,
            damageTaken,
            coinGained,
            difficultyAdded,
            newDifficulty
        );

        //TODO ADD Breakdown of How difficulty is calculated
    }
}

[Serializable]
public struct CombatPositionsAndPrefabs
{
    [SerializeField]
    SPUM_Prefabs mainCharacterPrefab;

    [SerializeField]
    Transform mainCharacterPosition;
    public Enemy enemyPrefab;
    public List<Transform> enemiesPossiblePositions;
    public Transform StartingPosition;
}

public struct DamageInfo
{
    public int initialdamage;
    public int actualdamage;
    public bool isLethal; //true if the damage is enough to kill the enemy
    public bool isExact; //true if the damage is exactly equal to the enemy health
    public bool isPerfect; //true if the damage is equal to the enemy health and all numberblocks are used
    public bool isSatisfied; // true if all requirement are met

    public int piercing; // Number of enemies that will be pierced

    //TO DO piercing, crit, etc

    public DamageInfo(int initialdamage)
    {
        this.initialdamage = initialdamage;
        this.actualdamage = initialdamage;
        this.isLethal = false;
        this.isExact = false;
        this.isSatisfied = false;
        this.isPerfect = false;
        this.piercing = 0;
    }

    public DamageInfo(int initialdamage, bool isPerfect)
    {
        this.initialdamage = initialdamage;
        this.actualdamage = initialdamage;
        this.isLethal = false;
        this.isExact = false;
        this.isSatisfied = false;
        this.isPerfect = isPerfect;
        this.piercing = 0;
    }
}

public enum GameState
{
    CombatInitialization,
    ArtifactInitiation,
    BeforePlayerTurn,
    PlayerTurn,
    AfterPlayerTurn,
    EnemyTurn,
    Waiting,
}
