using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBlockZone : Zone
{
    [SerializeField]
    GameObject Highlight;

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

    public override void AddBlockToZone(NumberBlock block)
    {
        NumberBlock numberBlock = (NumberBlock)block;
        if (!numberBlock.isInContainer)
        {
            numberBlock.transform.SetParent(this.transform, true);
            numberBlock.SetOriginalPosition(
                new Vector2(NumberBlocksManager.Instance.numberBlocksInContainer * 2f + 1.5f, 0.15f)
            );
            numberBlock.PutBackToOriginalPosition();
        }

        numbers.Add(numberBlock);
        block.zone = this;
        NumberBlocksManager.Instance.numberBlocksInContainer++;
    }

    public override void RemoveBlockFromZone(NumberBlock numberBlock)
    {
        numberBlock.isInContainer = false;
        numbers.Remove(numberBlock);
        int i = 0;
        foreach (var number in numbers)
        {
            number.SetLocalPosition(new Vector2(i * 2f + 1.5f, 0.15f));
            number.SetOriginalPosition();
            i++;
        }
        NumberBlocksManager.Instance.numberBlocksInContainer--;
    }
}
