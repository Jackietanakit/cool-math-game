using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public TextMeshPro OperatorNameText;
    public TextMeshPro OperatorDescriptionText;

    public TextMeshPro OperatorOtherInfoText;

    void Awake()
    {
        Instance = this;
    }

    public void CreateOperatorBlockAtContainer(OperationCard operationCard)
    {
        //spawn operator at the postions
        OperatorBlock operatorBlock = Instantiate(
            OperatorBlockPrefab,
            OperatorBlockPosition[operatorBlocksInContainer]
        );

        operatorBlock.Initialize(operationCard, OperatorBlockPosition[operatorBlocksInContainer]);
        //set position
        operatorBlock.SetLocalPosition(new Vector3(0.15f, 0.15f, 0));
        operatorBlocksInContainer++;
    }

    public void CreateManyOperators(List<OperationCard> operationCards)
    {
        foreach (OperationCard operationCard in operationCards)
        {
            CreateOperatorBlockAtContainer(operationCard);
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

        //Update the Text
        UpdateOperatorTextDescription();
    }

    public void UpdateOperatorTextDescription()
    {
        OperatorNameText.text = SelectedOperatorBlock.OperatorName.ToString();
        OperatorDescriptionText.text = SelectedOperatorBlock.OperatorDescription;
    }
}
