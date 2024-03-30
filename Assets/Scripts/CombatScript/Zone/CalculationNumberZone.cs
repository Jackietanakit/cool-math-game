using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationNumberZone : Zone
{
    public override void OnNumberBlockStay(NumberBlock numberBlock)
    {
        // if the number is in the zone then add the number to the list
        if (Input.GetMouseButtonUp(0) && numbers.Count == 0 && !numbers.Contains(numberBlock))
        {
            //Put the number block into the zone
            MoveBlockToThisZone(numberBlock);
            numberBlock.isInContainer = false;
        }
    }

    public override void AddBlockToZone(Block block)
    {
        base.AddBlockToZone(block);
        //but the block into the center
        block.SetOriginalPosition(new Vector2(0.15f, 0.15f));
    }

    public void ResetNumber()
    {
        numbers.Clear();
    }
}
