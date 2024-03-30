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

    //Ontrigger block collide into the zone

    void Update()
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

            if (block is OperatorBlock && AcceptOperators)
            {
                OperatorBlock operatorBlock = (OperatorBlock)block;
                if (operatorBlock != null)
                {
                    OnOperatorBlockStay(operatorBlock);
                }
            }
        }
    }

    public virtual void OnNumberBlockStay(NumberBlock numberBlock)
    {
        Debug.Log("NumberBlock Stay");
    }

    public virtual void OnOperatorBlockStay(OperatorBlock operatorBlock)
    {
        Debug.Log("OperatorBlock Stay");
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

    public virtual void AddBlockToZone(Block block)
    {
        if (!CanAccept(block))
        {
            return;
        }
        if (block is NumberBlock)
        {
            numbers.Add((NumberBlock)block);
        }
        else if (block is OperatorBlock)
        {
            operators.Add((OperatorBlock)block);
        }
        //move parent to this zone
        block.transform.SetParent(this.transform);

        block.zone = this;
    }

    public virtual void RemoveBlockFromZone(Block block)
    {
        if (block is NumberBlock)
        {
            numbers.Remove((NumberBlock)block);
        }
        else if (block is OperatorBlock)
        {
            operators.Remove((OperatorBlock)block);
        }
    }

    public virtual void MoveBlockToThisZone(Block block)
    {
        if (CanAccept(block))
        {
            block.zone.RemoveBlockFromZone(block);
            AddBlockToZone(block);
        }
    }
}
