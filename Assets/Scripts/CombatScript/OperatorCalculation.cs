using UnityEngine;

public class OperatorCalculation
{
    public static Operation CreateOperation(OperationName name)
    {
        switch (name)
        {
            case OperationName.Add:
                return new AddOperation();
            case OperationName.Subtract:
                return new SubtractOperation();
            case OperationName.Multiply:
                return new MultiplyOperation();
            case OperationName.Divide:
                return new DivideOperation();
            case OperationName.Modulo:
                return new ModuloOperation();
            case OperationName.Power:
                return new PowerOperation();
            case OperationName.Sqrt:
                return new SqrtOperation();
            case OperationName.Factorial:
                return new FactorialOperation();
            default:
                return null;
        }
    }
}

public enum OperatorType
{
    Binary,
    UnaryLeft, // Unary that only accept number a
    UnaryRight, // Unary that only accept number b
}

public abstract class Operation
{
    public OperatorType operatorType;
    public int a;
    public int b;

    public abstract int Calculate();
}

public class AddOperation : Operation
{
    //on creating a new add operation, set the operator type to binary
    public AddOperation()
    {
        operatorType = OperatorType.Binary;
    }

    public override int Calculate() => a + b;
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

// concatenate numbers
public class ConcatenateOperation : Operation
{
    public ConcatenateOperation()
    {
        operatorType = OperatorType.Binary;
    }

    public override int Calculate()
    {
        return int.Parse(a.ToString() + b.ToString());
    }
}
