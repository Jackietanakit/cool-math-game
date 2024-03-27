using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorBlockManager : MonoBehaviour
{
    public static OperatorBlockManager Instance;

    public RectTransform OperatorBlockContainer;
    public OperatorBlock OperatorBlockPrefab;

    public List<OperatorBlock> operatorBlocks;

    public int operatorBlocksInContainer = 0;

    public int maxoperatorBlocksInContainer = 10;

    void Awake()
    {
        Instance = this;
    }

    public void AddOperatorBlock(OperatorBlock operatorBlock)
    {
        operatorBlocks.Add(operatorBlock);
    }

    public void RemoveOperatorBlock(OperatorBlock operatorBlock)
    {
        operatorBlocks.Remove(operatorBlock);
    }

    public OperatorBlock GetOperatorBlock(string name)
    {
        foreach (OperatorBlock operatorBlock in operatorBlocks)
        {
            if (operatorBlock.Name == name)
            {
                return operatorBlock;
            }
        }
        return null;
    }

    public void CreateOperatorBlockAtContainer(OperationName name)
    {
        OperatorBlock operatorBlock = Instantiate(
            OperatorBlockPrefab,
            OperatorBlockContainer,
            true
        );
        PutOperatorBlockIntoContainer(operatorBlock);
        operatorBlock.Initialize(name);
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

    private void PutOperatorBlockIntoContainer(OperatorBlock operatorBlock)
    {
        operatorBlock.transform.SetParent(OperatorBlockContainer, true);
        operatorBlock.transform.localPosition = new Vector2(
            (0.055f - operatorBlock.RectPosition.anchoredPosition.x)
                + operatorBlocksInContainer * 0.09f,
            0.15f
        );
        operatorBlocksInContainer++;
    }
}
