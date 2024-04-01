using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationOperatorZone : Zone
{
    public override void OnOperatorBlockStay(OperatorBlock operatorBlock)
    {
        // if the number is in the zone then add the number to the list
        if (Input.GetMouseButtonUp(0) && operators.Count == 0 && !operators.Contains(operatorBlock))
        {
            //Put the number block into the zone
            MoveBlockToThisZone(operatorBlock);
            operatorBlock.isInContainer = false;
        }
    }

    public override void AddBlockToZone(Block block)
    {
        base.AddBlockToZone(block);
        //but the block into the center
        block.SetOriginalPosition(new Vector2(0.15f, 0.15f));
    }

    public void ResetOperator()
    {
        operators.Clear();
    }
}
