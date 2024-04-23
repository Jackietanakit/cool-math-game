using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerZone : Zone
{
    public override void OnNumberBlockStay(NumberBlock numberBlock)
    {
        return;
    }

    public override void AddBlockToZone(NumberBlock block)
    {
        base.AddBlockToZone(block);
        //but the block into the center
        block.SetOriginalPosition(new Vector2(0.125f, 0.125f));
        block.SetLocalPosition(new Vector2(0.125f, 0.125f));
        numbers.Add(block);
        block.isInContainer = false;
    }
}
