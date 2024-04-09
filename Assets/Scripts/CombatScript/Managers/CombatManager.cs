using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    public NumberBlockZone NumberBlockZone;

    public Enemy enemyPrefab;

    public GameObject EnemyContainer; //The parent of the enemies

    public DamageZone damageZone;

    public bool hasDraggedSomething;

    public List<Enemy> enemies = new List<Enemy>();

    public List<Transform> enemiesPossiblePositions;

    public List<Zone> zones;

    void Start()
    {
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

        CreateEnemy();
    }

    private List<OperationCard> operationCards = new List<OperationCard>();

    void Awake()
    {
        Instance = this;
    }

    public void CreateEnemy()
    {
        //Instantiate enemy at position 0
        Enemy enemy = Instantiate(enemyPrefab, enemiesPossiblePositions[0], true);
        enemy.transform.localPosition = new Vector3(0, 0.5f, 0);
        enemy.Initialize(10, 70, new List<Enemy.Requirement> { Enemy.Requirement.Exact });
        enemies.Add(enemy);
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
            damageZone.ResetNumber();
            //Damage Enemy at the closest distance to the player, using enemies
            enemies[0].TakeDamage(damage);
            //
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
        NumberBlocksManager.Instance.NextTurn();
        CalculationManager.Instance.ClearAll();
    }
}
