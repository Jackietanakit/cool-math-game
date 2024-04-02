using UnityEngine;

public class OperatorBlock : Block
{
    /*Operator has 2 types, binary and unary, binary operator require 2 number blocks, unary operator has 1 number block,
    the operator has the function to calculate the result of the operation. The function have to check whether the operation is valid or not.
    */
    public SpriteRenderer OperatorSprite;

    public NumberBlock numberBlockA;
    public NumberBlock numberBlockB;

    public Operation operation;

    public string Name { get; set; }

    public string Description { get; set; }

    public void Initialize(OperationName name, Zone zone)
    {
        this.zone = zone;
        SetOrderInLayer(2);
        SetOperation(name);
        normalScale = transform.lossyScale;
        this.tag = "OperatorBlock";
    }

    public OperatorType GetOperatorType()
    {
        return operation.operatorType;
    }

    public void SetNumberBlockA(NumberBlock numberBlock)
    {
        SetNumberBlock(ref numberBlockA, numberBlock, SetValueA);
    }

    public void SetNumberBlockB(NumberBlock numberBlock)
    {
        SetNumberBlock(ref numberBlockB, numberBlock, SetValueB);
    }

    private void SetNumberBlock(
        ref NumberBlock block,
        NumberBlock newBlock,
        System.Action<int> setValueAction
    )
    {
        if (newBlock != null)
        {
            block = newBlock;
            setValueAction(block.GetNumber());
        }
    }

    private void SetValueA(int value)
    {
        operation.a = value;
    }

    private void SetValueB(int value)
    {
        operation.b = value;
    }

    public int Calculate()
    {
        return operation.Calculate();
    }

    private void SetOperation(OperationName name)
    {
        operation = OperatorCalculation.CreateOperation(name);
        OperatorSprite.sprite = Resources.Load<Sprite>("Operators/" + name.ToString());
    }

    public override void SetOrderInLayer(int orderinLayer)
    {
        base.SetOrderInLayer(orderinLayer);
        OperatorSprite.sortingOrder = orderinLayer + 2;
    }
}
