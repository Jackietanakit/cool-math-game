using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberBlock : MonoBehaviour
{
    public int number;

    public bool isSelected = false;
    public bool hasGhost = false;
    public RectTransform RectPosition;

    public GameObject GhostBlockPrefab;

    public Vector2 normalScale;

    public Color normalColor;

    public Color hoverColor;
    public TextMeshPro textMesh;

    public GameObject BorderOnHover;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;

    void Update()
    {
        // if mouse hover on the number block, the number block will be bigger
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (boxCollider.OverlapPoint(mousePosition))
        {
            OnHover();
        }
        else
        {
            transform.localScale = normalScale;
            BorderOnHover.SetActive(false);
        }

        //if mouse drag on the number block, the number block will be moved to the mouse position, however, there are still a ghost number block at the original position
        if (Input.GetMouseButton(0))
        {
            if (
                boxCollider.OverlapPoint(mousePosition)
                && (CombatManager.Instance.hasDraggedSomething == false || isSelected)
            )
            {
                isSelected = true;
                CombatManager.Instance.hasDraggedSomething = true;
                if (!hasGhost)
                {
                    GameObject ghostBlock = Instantiate(
                        GhostBlockPrefab,
                        transform.position,
                        Quaternion.identity
                    );
                }
                hasGhost = true;
                transform.position = mousePosition;
            }
        }

        //if mouse release the number block, the number block will be destroyed
        if (Input.GetMouseButtonUp(0))
        {
            CombatManager.Instance.hasDraggedSomething = false;
            isSelected = false;
        }
    }

    public void Initialize(int number)
    {
        SetNumber(number);
        normalScale = transform.localScale;
    }

    public void SetNumber(int number)
    {
        this.number = number;
        textMesh.text = number.ToString();
    }

    public void OnHover()
    {
        transform.localScale = normalScale * 1.2f;
        BorderOnHover.SetActive(true);
    }
}
