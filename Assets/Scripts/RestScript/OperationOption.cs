using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationOption : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D deleteBoxCollider;

    [SerializeField]
    Color color;
    public SpriteRenderer deleteSpriteRenderer;
    public SpriteRenderer operationSpriteRenderer;
    public Transform ParentTransform;
    public OperationCard operationCard;

    public void Initialize(OperationCard card, Transform parent)
    {
        operationCard = card;
        ParentTransform = parent.transform;
        operationSpriteRenderer.sprite = Resources.Load<Sprite>("Operators/" + name.ToString());
        tag = "OperatorOption";
        name = "OperatorOption" + name;
    }

    void Update()
    {
        if (deleteBoxCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            deleteSpriteRenderer.color = Color.gray;
            if (Input.GetMouseButtonUp(0))
            {
                Debug.LogWarning("Kuay Pit");
            }
        }
        else
        {
            deleteSpriteRenderer.color = Color.white;
        }
    }
}
