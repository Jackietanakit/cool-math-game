using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CalculationNumberZone : Zone
{
    // public override void OnNumberBlockStay(NumberBlock numberBlock)
    // {
    //     if (numbers.Count == 0 && !numbers.Contains(numberBlock) && Input.GetMouseButtonUp(0))
    //     {
    //         //Put the number block into the zone
    //         Debug.Log("Move to zone" + this.name);
    //         MoveBlockToThisZone(numberBlock);
    //     }
    //     // if the number is in the zone then add the number to the list
    // }

    public override void AddBlockToZone(NumberBlock block)
    {
        base.AddBlockToZone(block);
        //but the block into the center
        block.SetOriginalPosition(new Vector2(0.15f, 0.15f));
    }

    public override void MoveBlockToThisZone(NumberBlock block)
    {
        base.MoveBlockToThisZone(block);
        block.isInContainer = false;
    }

    public void ResetNumber()
    {
        numbers.Clear();
    }
}
