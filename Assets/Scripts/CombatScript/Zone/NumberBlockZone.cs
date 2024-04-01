using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBlockZone : Zone
{
    public override void OnNumberBlockStay(NumberBlock numberBlock)
    {
        // if the number is not in container and the mouse stop left click then put the number block into the container
        if (
            !numberBlock.isInContainer
            && Input.GetMouseButtonUp(0)
            && !numbers.Contains(numberBlock)
        )
        {
            MoveBlockToThisZone(numberBlock);
            numberBlock.isInContainer = true;
        }
    }

    public override void AddBlockToZone(Block block)
    {
        NumberBlock numberBlock = (NumberBlock)block;
        if (!numberBlock.isInContainer)
        {
            numberBlock.transform.SetParent(this.transform, true);
            numberBlock.SetOriginalPosition(new Vector2(numbers.Count * 0.09f - 0.4f, 0.15f));
            numberBlock.PutBackToOriginalPosition();
        }

        numbers.Add(numberBlock);
        block.zone = this;
    }

    public override void RemoveBlockFromZone(Block block)
    {
        NumberBlock numberBlock = (NumberBlock)block;
        numberBlock.isInContainer = false;
        numbers.Remove(numberBlock);
        int i = 0;
        foreach (var number in numbers)
        {
            number.SetLocalPosition(new Vector2(i * 0.09f - 0.4f, 0.15f));
            number.SetOriginalPosition();
            i++;
        }
    }
}
