using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorBlock : Block
{
    /*Operator has 2 types, binary and unary, binary operator require 2 number blocks, unary operator has 1 number block,
    the operator has the function to calculate the result of the operation. The function have to check whether the operation is valid or not.
    */
    public Sprite OperatorSprite;
    public OperatorType operatorType;

    public NumberBlock numberBlockA;
    public NumberBlock numberBlockB;

    public Operation operation;

    public string Name { get; set; }

    //function to calculate the result of the operation
    public int Calculate()
    {
        return 0;
    }

    public enum OperatorType
    {
        Binary,
        Unary
    }

    public OperatorType GetOperatorType()
    {
        return operatorType;
    }

    public void SetValue(int a)
    {
        this.operation.a = a;
    }

    public void SetValue(int a, int b)
    {
        this.operation.a = a;
        this.operation.b = b;
    }
}

// all of the operator functions

public class Operation
{
    public int a;
    public int b;

    //function calculate can take 1 or 2 numbers and calculate the result, it is avirtual function, so it can be overriden by the child class
    public virtual int Calculate()
    {
        return 0;
    }
}
