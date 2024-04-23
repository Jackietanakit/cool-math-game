using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateButton : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D boxCollider;

    [SerializeField]
    GameObject highlightObject;

    void Update()
    {
        if (boxCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            onHover();
            if (Input.GetMouseButtonUp(0))
            {
                CalculationManager.Instance.Calculate();
            }
        }
        else
        {
            onExit();
        }
    }

    void onHover()
    {
        highlightObject.SetActive(true);
    }

    void onExit()
    {
        highlightObject.SetActive(false);
    }
}
