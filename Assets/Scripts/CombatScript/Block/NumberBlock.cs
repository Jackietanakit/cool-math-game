using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberBlock : Block
{
    public int number;
    public TextMeshPro textMesh;

    //TO DO : Have an extra effect on using the number block

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

    public override void SetOrderInLayer(int orderinLayer)
    {
        base.SetOrderInLayer(orderinLayer);
        textMesh.sortingOrder = orderinLayer + 2;
    }

    //TO DO Find all information about the number, such as multiples, isprime?, etc
}
