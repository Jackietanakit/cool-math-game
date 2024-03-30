using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorBlockManager : MonoBehaviour
{
    public static OperatorBlockManager Instance;

    public RectTransform OperatorBlockContainer;
    public OperatorBlockZone OperatorBlockZone;
    public OperatorBlock OperatorBlockPrefab;

    public int operatorBlocksInContainer = 0;

    public int maxoperatorBlocksInContainer = 10;

    public List<OperatorBlock> operatorBlocks = new List<OperatorBlock>();

    void Awake()
    {
        Instance = this;
    }

    public void CreateOperatorBlockAtContainer(OperationName name)
    {
        OperatorBlock operatorBlock = Instantiate(
            OperatorBlockPrefab,
            OperatorBlockContainer,
            true
        );
        OperatorBlockZone.AddBlockToZone(operatorBlock);
        operatorBlock.Initialize(name, OperatorBlockZone);
        operatorBlock.SetOriginalPosition();
        operatorBlocks.Add(operatorBlock);
    }

    public void CreateManyOperators(List<OperationName> names)
    {
        foreach (OperationName name in names)
        {
            CreateOperatorBlockAtContainer(name);
        }
    }
}
