using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    public bool hasDraggedSomething;

    void Start()
    {
        NumberBlocksManager.Instance.CreateManyNumberBlocks(
            new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
        );
        OperatorBlockManager.Instance.CreateManyOperators(
            new List<OperationName>
            {
                OperationName.Add,
                OperationName.Subtract,
                OperationName.Multiply,
                OperationName.Divide
            }
        );
    }

    void Awake()
    {
        Instance = this;
    }

    public Block GetBlockUnderMouse()
    {
        foreach (NumberBlock numberBlock in NumberBlocksManager.Instance.numberBlocks)
        {
            //if block is selected
            if (numberBlock.isSelected)
            {
                return numberBlock;
            }
        }

        foreach (OperatorBlock operatorBlock in OperatorBlockManager.Instance.operatorBlocks)
        {
            if (operatorBlock.isSelected)
            {
                return operatorBlock;
            }
        }

        return null;
    }
}
