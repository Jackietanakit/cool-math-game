using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class CalculationManager : MonoBehaviour
{
    public CalculationNumberZone numberBlockA;
    public OperatorBlock Operator;
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

    public void ResetOperator()
    {
        Operator = null;
        OperatorBlockManager.Instance.SelectedSpriteRenderer.sprite = null;
    }

    public bool IsValidEquation()
    {
        //check binary,unaryleft,unaryright
        if (Operator != null)
        {
            if (Operator.GetOperatorType() == OperatorType.Binary)
            {
                if (GetNumberBlockA() != null && GetNumberBlockB() != null)
                {
                    return true;
                }
            }
            //unaryleft
            else if (Operator.GetOperatorType() == OperatorType.UnaryLeft)
            {
                if (GetNumberBlockA() != null && GetNumberBlockB() == null)
                {
                    return true;
                }
            }
            //unaryright
            else if (Operator.GetOperatorType() == OperatorType.UnaryRight)
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
        // check for case for binary, unaryleft, unaryright and use setNumberBlock in the operator
        if (Operator != null && IsValidEquation())
        {
            if (Operator.operation.operatorType == OperatorType.Binary)
            {
                Operator.SetNumberBlockA(GetNumberBlockA());
                Operator.SetNumberBlockB(GetNumberBlockB());
            }
            else if (Operator.operation.operatorType == OperatorType.UnaryLeft)
            {
                Operator.SetNumberBlockA(GetNumberBlockA());
            }
            else if (Operator.operation.operatorType == OperatorType.UnaryRight)
            {
                Operator.SetNumberBlockB(GetNumberBlockB());
            }
        }
    }

    public void Calculate()
    {
        if (IsValidEquation())
        {
            //calculate the result
            int result = Operator.operation.Calculate();
            NumberBlocksManager.Instance.CreateAnswerNumberBlock(result);

            //remove the number blocks also from the manager

            ClearAll();
        }
    }

    public void ClearAll()
    {
        if (GetNumberBlockA() != null)
        {
            NumberBlocksManager.Instance.RemoveNumberBlockFromList(GetNumberBlockA());
            GetNumberBlockA().RemoveBlock();
            numberBlockA.ResetNumber();
        }

        if (GetNumberBlockB() != null)
        {
            NumberBlocksManager.Instance.RemoveNumberBlockFromList(GetNumberBlockB());
            GetNumberBlockB().RemoveBlock();
            numberBlockB.ResetNumber();
        }
        if (Operator != null)
        {
            ResetOperator();
        }
    }
}
