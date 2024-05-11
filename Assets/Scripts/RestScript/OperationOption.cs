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
    public int orderInLayer = 2;

    public void Initialize(OperationCard card)
    {
        operationCard = card;
        operationSpriteRenderer.sprite = Resources.Load<Sprite>(
            "Operators/" + card.operationName.ToString()
        );
        deleteSpriteRenderer.sprite = Resources.Load<Sprite>("Operators/Add");
        SetOrderInLayer(2);
    }

    void Update()
    {
        if (deleteBoxCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            deleteSpriteRenderer.color = Color.gray;
            if (Input.GetMouseButtonUp(0))
            {
                RewardManager.Instance.RemoveOperationCard(operationCard);
                Destroy(gameObject);
                RewardManager.Instance.AddOperationCard();
                ScenesManager.Instance.LoadMapScene();
            }
        }
        else
        {
            deleteSpriteRenderer.color = Color.white;
        }
    }

    public void SetLocalPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public virtual void SetOrderInLayer(int orderinLayer)
    {
        this.orderInLayer = orderinLayer;
        operationSpriteRenderer.sortingOrder = orderinLayer + 1;
        deleteSpriteRenderer.sortingOrder = orderinLayer + 2;
    }
}
