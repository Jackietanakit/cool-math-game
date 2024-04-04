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

    public Zone zone;

    public bool isInContainer;

    public bool isSelected = false;
    public int orderInLayer = 2;

    public bool hasGhost = false;

    void Start()
    {
        normalScale = transform.lossyScale;
        this.tag = "Block";
    }

    void Update()
    {
        // if mouse hover on the block, the block will be bigger
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
            //set local scale according to lossy scale factor divided by parent lossy scale factor
            SetScale(normalScale);
            BorderOnHover.SetActive(false);
        }

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
                return;
            }
            else
            {
                Debug.Log("Put back to original position");
                PutBackToOriginalPosition();
            }
        }
        if (hasGhost && !isSelected)
        {
            Debug.Log("Put back to original position");
            PutBackToOriginalPosition();
        }
    }

    public void SetOriginalPosition()
    {
        OriginalPosition = transform.localPosition;
    }

    public void SetOriginalPosition(Vector2 position)
    {
        OriginalPosition = position;
    }

    public void PutBackToOriginalPosition()
    {
        hasGhost = false;
        SetLocalPosition(OriginalPosition);
        Destroy(GhostBlock);
    }

    public void OnHover()
    {
        SetScale(normalScale * 1.2f);
        BorderOnHover.SetActive(true);
    }

    public void SetLocalPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public virtual void SetOrderInLayer(int orderinLayer)
    {
        this.orderInLayer = orderinLayer;
        spriteRenderer.sortingOrder = orderinLayer + 1;
        BorderOnHover.GetComponent<SpriteRenderer>().sortingOrder = orderinLayer;
    }

    public void SetScale(Vector2 scale)
    {
        transform.localScale = scale / zone.transform.lossyScale;
    }

    public void RemoveBlock()
    {
        Destroy(gameObject);
    }
}
