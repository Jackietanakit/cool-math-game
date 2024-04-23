using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorBlockManager : MonoBehaviour
{
    public static OperatorBlockManager Instance;

    public List<Transform> OperatorBlockPosition;
    public OperatorBlock OperatorBlockPrefab;

    public OperatorBlock SelectedOperatorBlock;
    public SpriteRenderer SelectedSpriteRenderer;

    public int operatorBlocksInContainer = 0;

    public int maxoperatorBlocksInContainer = 5;

    public List<OperatorBlock> operatorBlocks = new List<OperatorBlock>();

    void Awake()
    {
        Instance = this;
    }

    public void CreateOperatorBlockAtContainer(OperationName name)
    {
        //spawn operator at the postions
        OperatorBlock operatorBlock = Instantiate(
            OperatorBlockPrefab,
            OperatorBlockPosition[operatorBlocksInContainer]
        );

        operatorBlock.Initialize(name, OperatorBlockPosition[operatorBlocksInContainer]);
        //set position
        operatorBlock.SetLocalPosition(new Vector3(0.15f, 0.15f, 0));
        operatorBlocksInContainer++;
    }

    public void CreateManyOperators(List<OperationName> names)
    {
        foreach (OperationName name in names)
        {
            CreateOperatorBlockAtContainer(name);
        }
    }

    public void SelectOperator(OperatorBlock operatorBlock)
    {
        if (
            SelectedOperatorBlock != null
            && SelectedOperatorBlock.GetOperatorType() != operatorBlock.GetOperatorType()
        )
        {
            //kick the numberblock back to the container
            if (CalculationManager.Instance.GetNumberBlockA() != null)
                NumberBlocksManager.Instance.NumberBlockZone.MoveBlockToThisZone(
                    CalculationManager.Instance.GetNumberBlockA()
                );
            if (CalculationManager.Instance.GetNumberBlockB() != null)
                NumberBlocksManager.Instance.NumberBlockZone.MoveBlockToThisZone(
                    CalculationManager.Instance.GetNumberBlockB()
                );
        }
        SelectedOperatorBlock = operatorBlock;
        SelectedSpriteRenderer.sprite = operatorBlock.OperatorSprite.sprite;
        CalculationManager.Instance.Operator = operatorBlock;
    }
}
