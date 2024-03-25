using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberBlock : Block
{
    public int number;
    public TextMeshPro textMesh;

    public void Initialize(int number)
    {
        SetNumber(number);
        normalScale = transform.localScale;
        SetOrderInLayer(2);
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

    public override void SetOrderInLayer(int orderinLayer)
    {
        this.orderInLayer = orderinLayer;
        spriteRenderer.sortingOrder = orderinLayer + 1;
        textMesh.sortingOrder = orderinLayer + 2;
        BorderOnHover.GetComponent<SpriteRenderer>().sortingOrder = orderinLayer;
    }
}
