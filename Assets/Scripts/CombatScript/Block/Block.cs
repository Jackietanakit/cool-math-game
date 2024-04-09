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

    public bool isInContainer;

    public bool isSelected = false;
    public int orderInLayer = 2;

    public bool hasGhost = false;

    void Start()
    {
        normalScale = transform.lossyScale;
        this.tag = "Block";
    }

    public virtual void Update()
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

    public virtual void SetScale(Vector2 scale) { }

    public virtual void RemoveBlock()
    {
        Destroy(gameObject);
    }
}
