using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorBlockZone : Zone
{
    public override void OnOperatorBlockStay(OperatorBlock operatorBlock)
    {
        // if the number is not in container and the mouse stop left click then put the number block into the container
        if (
            !operatorBlock.isInContainer
            && Input.GetMouseButtonUp(0)
            && !operators.Contains(operatorBlock)
        )
        {
            MoveBlockToThisZone(operatorBlock);
            operatorBlock.isInContainer = true;
        }
    }

    public override void AddBlockToZone(Block block)
    {
        OperatorBlock opratorBlock = (OperatorBlock)block;
        if (!opratorBlock.isInContainer)
        {
            opratorBlock.transform.SetParent(transform, true);
            opratorBlock.SetOriginalPosition(new Vector2(operators.Count * 0.09f - 0.4f, 0.15f));
            opratorBlock.PutBackToOriginalPosition();
        }

        operators.Add(opratorBlock);
        block.zone = this;
    }

    public override void RemoveBlockFromZone(Block block)
    {
        OperatorBlock opratorBlock = (OperatorBlock)block;
        opratorBlock.isInContainer = false;
        operators.Remove(opratorBlock);
        int i = 0;
        foreach (var operatorBlock in operators)
        {
            operatorBlock.SetLocalPosition(new Vector2(i * 0.09f - 0.4f, 0.15f));
            operatorBlock.SetOriginalPosition();
            i++;
        }
    }
}
