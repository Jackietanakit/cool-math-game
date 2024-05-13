using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberBlock : Block
{
    public Zone zone;
    public int number;
    public TextMeshPro textMesh;

    //TO DO : Have an extra effect on using the number block

    public override void Update()
    {
        base.Update();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //if mouse drag on the block, the block will be moved to the mouse position, however, there are still a ghost number block at the original position
        if (
            isSelected
            || (
                Input.GetMouseButton(0)
                && boxCollider.OverlapPoint(mousePosition)
                && (CombatManager.Instance.hasDraggedSomething == false)
            )
        )
        {
            SetOrderInLayer(4);
            isSelected = true;
            CombatManager.Instance.hasDraggedSomething = true;
            CombatManager.Instance.DraggedNumberBlock = this;
            if (!hasGhost)
            {
                GameObject ghostBlock = Instantiate(
                    GhostBlockPrefab,
                    transform.position,
                    Quaternion.identity
                );
                GhostBlock = ghostBlock;
            }
            hasGhost = true;
            SetPosition(mousePosition);
        }
        //if mouse release the number block, the number block will be destroyed
        if (Input.GetMouseButtonUp(0) && isSelected)
        {
            SetOrderInLayer(2);
            CombatManager.Instance.hasDraggedSomething = false;
            CombatManager.Instance.DraggedNumberBlock = null;
            isSelected = false;
            Zone newzone = CombatManager.Instance.GetZoneUnderMouse();

            //if the number block is in another zone, the number block will be destroyed and the number will be added to the zone, else the block will be put back to the original position
            if (
                newzone != null
                && !(newzone == zone)
                && newzone.CanAccept(this)
                && newzone is not AnswerZone
            )
            {
                //Put it in the zone
                newzone.MoveBlockToThisZone(this);

                return;
            }
            else
            {
                PutBackToOriginalPosition();
            }
        }
        if (hasGhost && !isSelected)
        {
            PutBackToOriginalPosition();
        }
    }

    public void Initialize(int number, Zone zone)
    {
        this.zone = zone;
        SetNumber(number);
        normalScale = transform.lossyScale;
        SetOrderInLayer(2);
        this.tag = "NumberBlock";
    }

    public void SetNumber(int number)
    {
        this.number = number;
        textMesh.text = number.ToString();
    }

    public int GetNumber()
    {
        return number;
    }

    public void ChangeNumber(int number)
    {
        //TO DO : Add an effect when the number is changed
        SetNumber(number);
    }

    public override void SetOrderInLayer(int orderinLayer)
    {
        base.SetOrderInLayer(orderinLayer);
        textMesh.sortingOrder = orderinLayer + 2;
    }

    public override void SetScale(Vector2 scale)
    {
        transform.localScale = scale / zone.transform.lossyScale;
    }

    public override void RemoveBlock()
    {
        base.RemoveBlock();
    }

    //TO DO Find all information about the number, such as multiples, isprime?, etc
}
