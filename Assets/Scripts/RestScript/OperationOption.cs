using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationOption : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D deleteBoxCollider;

    [SerializeField]
    Color color;
    public Button deleteButton;
    public Image operationImage;
    public OperationCard operationCard;

    public void Start()
    {
        deleteButton.onClick.AddListener(() =>
        {
            GameManager.instance._playerInventory.RemoveOperationCard(operationCard);
            Destroy(gameObject);
            RewardManager.Instance.AddOperationCard();
            ScenesManager.Instance.LoadMapScene();
        });
    }

    public void Initialize(OperationCard card)
    {
        operationCard = card;
        operationImage.sprite = Resources.Load<Sprite>(
            "Operators/" + card.operationName.ToString()
        );
    }

    public void SetLocalPosition(Vector2 position)
    {
        transform.localPosition = position;
    }
}
