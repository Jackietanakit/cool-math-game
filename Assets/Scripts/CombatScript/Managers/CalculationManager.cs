using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class CalculationManager : MonoBehaviour
{
    public CalculationNumberZone numberBlockA;
    public CalculationOperatorZone operatorBlock;
    public CalculationNumberZone numberBlockB;

    public AnswerZone AnswerZone;

    public static CalculationManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        SetNumberInOperator();
    }

    public NumberBlock GetNumberBlockA()
    {
        return numberBlockA.numbers.Count == 1 ? numberBlockA.numbers[0] : null;
    }

    public NumberBlock GetNumberBlockB()
    {
        return numberBlockB.numbers.Count == 1 ? numberBlockB.numbers[0] : null;
    }

    public OperatorBlock GetOperatorBlock()
    {
        return operatorBlock.operators.Count == 1 ? operatorBlock.operators[0] : null;
    }

    public bool IsValidEquation()
    {
        //check binary,unaryleft,unaryright
        if (GetOperatorBlock() != null)
        {
            if (GetOperatorBlock().GetOperatorType() == OperatorType.Binary)
            {
                if (GetNumberBlockA() != null && GetNumberBlockB() != null)
                {
                    return true;
                }
            }
            //unaryleft
            else if (GetOperatorBlock().GetOperatorType() == OperatorType.UnaryLeft)
            {
                if (GetNumberBlockA() != null && GetNumberBlockB() == null)
                {
                    return true;
                }
            }
            //unaryright
            else if (GetOperatorBlock().GetOperatorType() == OperatorType.UnaryRight)
            {
                if (GetNumberBlockB() != null && GetNumberBlockA() == null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void SetNumberInOperator()
    {
        //check for case for binary, unaryleft, unaryright and use setNumberBlock in the operator
        if (GetOperatorBlock() != null && IsValidEquation())
        {
            OperatorBlock operatorBlock = GetOperatorBlock();
            if (operatorBlock.operation.operatorType == OperatorType.Binary)
            {
                operatorBlock.SetNumberBlockA(GetNumberBlockA());
                operatorBlock.SetNumberBlockB(GetNumberBlockB());
            }
            else if (operatorBlock.operation.operatorType == OperatorType.UnaryLeft)
            {
                operatorBlock.SetNumberBlockA(GetNumberBlockA());
            }
            else if (operatorBlock.operation.operatorType == OperatorType.UnaryRight)
            {
                operatorBlock.SetNumberBlockB(GetNumberBlockB());
            }
        }
    }

    public void Calculate()
    {
        if (IsValidEquation())
        {
            //calculate the result
            int result = GetOperatorBlock().operation.Calculate();
            NumberBlocksManager.Instance.CreateAnswerNumberBlock(result);

            //remove the number blocks also from the manager

            if (GetNumberBlockA() != null)
            {
                GetNumberBlockA().RemoveBlock();
                numberBlockA.ResetNumber();
            }

            if (GetNumberBlockB() != null)
            {
                GetNumberBlockB().RemoveBlock();
                numberBlockB.ResetNumber();
            }
            if (GetOperatorBlock() != null)
            {
                GetOperatorBlock().RemoveBlock();
                this.operatorBlock.ResetOperator();
            }

            //remove the number blocks in manager
        }
    }
}
