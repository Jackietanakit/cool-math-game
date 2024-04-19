using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Zone : MonoBehaviour
{
    public BoxCollider2D boxCollider;

    public bool AcceptNumbers;
    public bool AcceptOperators;

    public List<NumberBlock> numbers;

    public List<OperatorBlock> operators;

    public int MaxNumberBlocks;
    public int MaxOperatorBlock;

    public GameObject HighlightObject = null;

    //Ontrigger block collide into the zone

    public virtual void Update()
    {
        //check if the block is in the zone
        if (boxCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            Block block = CombatManager.Instance.GetBlockUnderMouse();
            if (block is NumberBlock && AcceptNumbers)
            {
                NumberBlock numberBlock = (NumberBlock)block;
                if (numberBlock != null)
                {
                    OnNumberBlockStay(numberBlock);
                }
            }
        }
        else
        {
            if (HighlightObject != null)
                HighlightObject.SetActive(false);
        }
    }

    public virtual void OnNumberBlockStay(NumberBlock numberBlock)
    {
        Debug.Log("NumberBlock Stay");
    }

    public bool IsInZone(Block block)
    {
        return boxCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public virtual bool CanAccept(Block block)
    {
        if (block is NumberBlock && numbers.Count < MaxNumberBlocks)
        {
            return AcceptNumbers;
        }
        else if (block is OperatorBlock && operators.Count < MaxOperatorBlock)
        {
            return AcceptOperators;
        }
        else
        {
            return false;
        }
    }

    public virtual void AddBlockToZone(NumberBlock block)
    {
        if (!CanAccept(block))
        {
            return;
        }
        if (block is NumberBlock)
        {
            numbers.Add((NumberBlock)block);
        }

        //move parent to this zone
        block.transform.SetParent(this.transform);

        block.zone = this;
    }

    public virtual void RemoveBlockFromZone(NumberBlock block)
    {
        numbers.Remove(block);
    }

    public virtual void MoveBlockToThisZone(NumberBlock block)
    {
        if (CanAccept(block))
        {
            block.zone.RemoveBlockFromZone(block);
            AddBlockToZone(block);
        }
    }
}
