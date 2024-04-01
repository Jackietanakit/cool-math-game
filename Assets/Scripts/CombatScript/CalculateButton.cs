using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateButton : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D boxCollider;

    void Update()
    {
        if (boxCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            if (Input.GetMouseButtonUp(0))
            {
                CalculationManager.Instance.Calculate();
            }
        }
    }
}
