using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public RectTransform RectPosition;
    public Vector2 OriginalPosition;
    public GameObject GhostBlockPrefab;
    public GameObject GhostBlock;
    public Vector2 normalScale;
    public Color normalColor;
    public Color hoverColor;
    public GameObject BorderOnHover;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;

    public bool isSelected = false;
    public int orderInLayer = 2;

    public bool hasGhost = false;

    void Update()
    {
        // if mouse hover on the number block, the number block will be bigger
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (
            boxCollider.OverlapPoint(mousePosition)
            && CombatManager.Instance.hasDraggedSomething == false
        )
        {
            OnHover();
        }
        else
        {
            transform.localScale = normalScale;
            BorderOnHover.SetActive(false);
        }

        //if mouse drag on the number block, the number block will be moved to the mouse position, however, there are still a ghost number block at the original position
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
            transform.position = mousePosition;
        }
        //if mouse release the number block, the number block will be destroyed
        if (Input.GetMouseButtonUp(0))
        {
            SetOrderInLayer(2);
            CombatManager.Instance.hasDraggedSomething = false;
            isSelected = false;

            //if the number block is in another zone, the number block will be destroyed and the number will be added to the zone, else the block will be put back to the original position
            if (NumberBlocksManager.Instance.CheckIfInZone(this))
            {
                NumberBlocksManager.Instance.AddNumberToZone(this);
                Destroy(gameObject);
            }
            else
            {
                transform.position = OriginalPosition;
                hasGhost = false;
                //destroy the ghost block
                Destroy(GhostBlock);
            }
        }
    }

    public void SetOriginalPosition()
    {
        OriginalPosition = transform.position;
    }

    public void OnHover()
    {
        transform.localScale = normalScale * 1.2f;
        BorderOnHover.SetActive(true);
    }

    public virtual void SetOrderInLayer(int orderinLayer)
    {
        this.orderInLayer = orderinLayer;
        spriteRenderer.sortingOrder = orderinLayer + 1;
        BorderOnHover.GetComponent<SpriteRenderer>().sortingOrder = orderinLayer;
    }
}
