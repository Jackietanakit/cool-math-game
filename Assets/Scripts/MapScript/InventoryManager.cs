using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Image imagePrefab;

    [SerializeField]
    TextMeshProUGUI MoneyText;

    [SerializeField]
    GridLayoutGroup operatorgrid;

    [SerializeField]
    GridLayoutGroup artifactgrid;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadOperator();
        LoadArtifact();
        MoneyText.text = GameManager.instance._playerInventory.money.ToString();
    }

    public void LoadOperator()
    {
        // Load all operators from GameManager
        List<OperationCard> operators = GameManager.instance._playerInventory.operationCards;
        foreach (OperationCard op in operators)
        {
            // Create OperatorInfo in OperatorPanel
            Image operatorImage = Instantiate(imagePrefab, operatorgrid.transform);
            operatorImage.sprite = Resources.Load<Sprite>(
                "Operators/" + op.operationName.ToString()
            );
        }
    }

    public void LoadArtifact()
    {
        // Load all artifacts from GameManager
        List<Artifact> artifacts = GameManager.instance._playerInventory.artifacts;
        foreach (Artifact artifact in artifacts)
        {
            // Create ArtifactInfo in ArtifactPanel
            Image artifactImage = Instantiate(imagePrefab, artifactgrid.transform);
            artifactImage.sprite = artifact.artifactSprite;
        }
    }
}
