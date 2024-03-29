using System.Collections;
using System.Collections.Generic;
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

    public string description { get; set; }

    public void Initialize(OperationName name, Zone zone)
    {
        this.zone = zone;
        SetOrderInLayer(2);
        setOperation(name);
        normalScale = transform.lossyScale;
        this.tag = "OperatorBlock";
    }

    public OperatorType GetOperatorType()
    {
        return operation.operatorType;
    }

    public void SetNumberBlockA(NumberBlock numberBlock)
    {
        if (numberBlock != null)
        {
            this.numberBlockA = numberBlock;
            SetValueA(numberBlock.GetNumber());
        }
    }

    public void SetNumberBlockB(NumberBlock numberBlock)
    {
        if (numberBlock != null)
        {
            this.numberBlockB = numberBlock;
            SetValueB(numberBlock.GetNumber());
        }
    }

    //remove block
    public void RemoveNumberBlockA()
    {
        this.numberBlockA = null;
    }

    public void RemoveNumberBlockB()
    {
        this.numberBlockB = null;
    }

    public void SetValueA(int a)
    {
        this.operation.a = a;
    }

    public void SetValueB(int b)
    {
        this.operation.b = b;
    }

    public int calculate()
    {
        return operation.Calculate();
    }

    public void setOperation(OperationName name)
    {
        switch (name)
        {
            case OperationName.Add:
                operation = new AddOperation();
                //load sprite
                OperatorSprite.sprite = Resources.Load<Sprite>("Operators/Add");
                break;
            case OperationName.Subtract:
                operation = new SubtractOperation();
                OperatorSprite.sprite = Resources.Load<Sprite>("Operators/Subtract");
                break;
            case OperationName.Multiply:
                operation = new MultiplyOperation();
                OperatorSprite.sprite = Resources.Load<Sprite>("Operators/Multiply");
                break;
            case OperationName.Divide:
                operation = new DivideOperation();
                OperatorSprite.sprite = Resources.Load<Sprite>("Operators/Divide");
                break;
            case OperationName.Modulo:
                operation = new ModuloOperation();
                break;
            case OperationName.Power:
                operation = new PowerOperation();
                break;
            case OperationName.Sqrt:
                operation = new SqrtOperation();
                break;
            case OperationName.Factorial:
                operation = new FactorialOperation();
                break;
        }
    }

    public override void SetOrderInLayer(int orderinLayer)
    {
        base.SetOrderInLayer(orderinLayer);
        OperatorSprite.sortingOrder = orderinLayer + 2;
    }
}

public enum OperatorType
{
    Binary,
    UnaryLeft, // Unary that only accept number a
    UnaryRight, // Unary that only accept number b
}

// all of the operator functions

//operation name enum
public enum OperationName
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Modulo,
    Power,
    Sqrt,
    Factorial
}

public abstract class Operation
{
    public OperatorType operatorType;
    public int a;
    public int b;

    //function calculate can take 1 or 2 numbers and calculate the result, it is avirtual function, so it can be overriden by the child class
    public abstract int Calculate();
}

public class AddOperation : Operation
{
    //on creating a new add operation, set the operator type to binary
    public AddOperation()
    {
        operatorType = OperatorType.Binary;
    }

    public override int Calculate()
    {
        return a + b;
    }
}

public class SubtractOperation : Operation
{
    public SubtractOperation()
    {
        operatorType = OperatorType.Binary;
    }

    public override int Calculate()
    {
        return a - b;
    }
}

public class MultiplyOperation : Operation
{
    public MultiplyOperation()
    {
        operatorType = OperatorType.Binary;
    }

    public override int Calculate()
    {
        return a * b;
    }
}

public class DivideOperation : Operation
{
    public DivideOperation()
    {
        operatorType = OperatorType.Binary;
    }

    public override int Calculate()
    {
        return a / b;
    }
}

public class ModuloOperation : Operation
{
    public ModuloOperation()
    {
        operatorType = OperatorType.Binary;
    }

    public override int Calculate()
    {
        return a % b;
    }
}

public class PowerOperation : Operation
{
    public PowerOperation()
    {
        operatorType = OperatorType.Binary;
    }

    public override int Calculate()
    {
        return (int)Mathf.Pow(a, b);
    }
}

public class SqrtOperation : Operation
{
    public SqrtOperation()
    {
        operatorType = OperatorType.UnaryRight;
    }

    public override int Calculate()
    {
        return (int)Mathf.Sqrt(b);
    }
}

//factorial
public class FactorialOperation : Operation
{
    public FactorialOperation()
    {
        operatorType = OperatorType.UnaryLeft;
    }

    public override int Calculate()
    {
        int result = 1;
        for (int i = 1; i <= a; i++)
        {
            result *= i;
        }
        return result;
    }
}
