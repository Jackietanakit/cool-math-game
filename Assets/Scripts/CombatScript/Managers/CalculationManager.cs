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
        //if operator is binary then wait for 2 number blocks, if unaryleft then wait for number a ,and vise versa
        if (GetOperatorBlock() != null)
        {
            OperatorBlock operatorBlock = GetOperatorBlock();
            operatorBlock.SetNumberBlockA(GetNumberBlockA());
            operatorBlock.SetNumberBlockB(GetNumberBlockB());

            if (GetOperatorBlock().GetOperatorType() == OperatorType.Binary)
            {
                if (GetNumberBlockA() != null && GetNumberBlockB() != null)
                {
                    Debug.Log("Calculation");
                    //calculate the result
                    int result = GetOperatorBlock().operation.Calculate();
                    Debug.Log(result);
                    NumberBlocksManager.Instance.CreateAnswerNumberBlock(result);
                    //remove the number blocks
                    GetNumberBlockA().RemoveBlock();
                    GetNumberBlockB().RemoveBlock();
                    GetOperatorBlock().RemoveBlock();

                    CombatManager.Instance.hasDraggedSomething = false;
                    numberBlockA.ResetNumber();
                    numberBlockB.ResetNumber();
                    this.operatorBlock.ResetOperator();
                }
            }
            // else if (GetOperatorBlock().GetOperatorType() == OperatorType.UnaryLeft)
            // {
            //     if (GetNumberBlockA() != null) { }
            // }
            // else if (GetOperatorBlock().GetOperatorType() == OperatorType.UnaryRight)
            // {
            //     if (GetNumberBlockB() != null) { }
            // }
        }
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
}
