using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DamageZone : Zone
{
    public override void OnNumberBlockStay(NumberBlock numberBlock)
    {
        // if (HighlightObject != null && numbers.Count == 0 && !numbers.Contains(numberBlock))
        // {
        //     if (Input.GetMouseButtonUp(0))
        //     {
        //         //Put the number block into the zone

        //         MoveBlockToThisZone(numberBlock);
        //         numberBlock.isInContainer = false;
        //     }
        // }
        // if the number is in the zone then add the number to the list
    }

    public override void AddBlockToZone(NumberBlock block)
    {
        base.AddBlockToZone(block);
        //but the block into the center
        block.SetOriginalPosition(new Vector2(0.125f, 0.125f));
        CombatManager.Instance.damageButton.updateText(block.GetNumber() + " Damage");
    }

    public override void RemoveBlockFromZone(NumberBlock block)
    {
        base.RemoveBlockFromZone(block);
        CombatManager.Instance.damageButton.setInactive();
    }

    public override void MoveBlockToThisZone(NumberBlock block)
    {
        base.MoveBlockToThisZone(block);
        block.isInContainer = false;
    }
}
