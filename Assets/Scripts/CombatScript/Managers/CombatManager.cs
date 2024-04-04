using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    public NumberBlockZone NumberBlockZone;
    public OperatorBlockZone OperatorBlockZone;

    public bool hasDraggedSomething;

    public List<Zone> zones;

    void Start()
    {
        NumberBlocksManager.Instance.CreateManyNumberBlocks(
            NumberBlocksManager.Instance.GenerateStartingRandomFairNumbers()
        );
        OperatorBlockManager.Instance.CreateManyOperators(
            new List<OperationName>
            {
                OperationName.Add,
                OperationName.Add,
                OperationName.Add,
                OperationName.Subtract,
                OperationName.Subtract,
                OperationName.Multiply,
                OperationName.Multiply,
                OperationName.Divide,
            }
        );
    }

    void Awake()
    {
        Instance = this;
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

    public void NextTurn()
    {
        // Generate random numbers until 9 blocks
        while (NumberBlockZone.numbers.Count < 9)
        {
            NumberBlocksManager.Instance.CreateNumberBlockAtContainer(
                NumberBlocksManager.Instance.GenerateRandomNumber()
            );
        }

        //generate 3 more operators or until reach 9 operators
        int i = 0;
        while (OperatorBlockZone.operators.Count < 9 && i < 3)
        {
            OperatorBlockManager.Instance.CreateOperatorBlockAtContainer(
                OperatorBlockManager.Instance.GenerateRandomOperator()
            );
            i++;
        }
    }
}
