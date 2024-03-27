using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Zone : MonoBehaviour
{
    public BoxCollider2D boxCollider;

    public bool AcceptNumbers;
    public bool AcceptOperators;

    public int MaxNumberBlocks;
    public int MaxOperatorBlock;

    //Ontrigger block collide into the zone

    void Update()
    {
        //check if the block is in the zone
        if (boxCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            //log the collision
            if (AcceptNumbers)
            {
                NumberBlock numberBlock = (NumberBlock)CombatManager.Instance.GetBlockUnderMouse();
                if (numberBlock != null)
                {
                    Debug.Log("number Collide");
                    OnNumberBlockStay(numberBlock);
                }
            }

            if (AcceptOperators)
            {
                OperatorBlock operatorBlock = (OperatorBlock)
                    CombatManager.Instance.GetBlockUnderMouse();
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
}
