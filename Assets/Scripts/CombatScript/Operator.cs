using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operator : MonoBehaviour
{
    /*Operator has 2 types, binary and unary, binary operator require 2 number blocks, unary operator has 1 number block, 
    the operator has the function to calculate the result of the operation. The function have to check whether the operation is valid or not.
    */
    public Sprite OperatorSprite;
    public OperatorType operatorType;
    public enum OperatorType
    {
        Binary,
        Unary
    }


}
