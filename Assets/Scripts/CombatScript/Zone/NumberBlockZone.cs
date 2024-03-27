using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBlockZone : Zone
{
    public override void OnNumberBlockStay(NumberBlock numberBlock)
    {
        // if the number is not in container and the mouse stop left click then put the number block into the container
        if (!numberBlock.isInContainer && Input.GetMouseButtonUp(0))
        {
            NumberBlocksManager.Instance.PutNumberBlockIntoContainer(numberBlock);
            numberBlock.isInContainer = true;
        }
    }
}
